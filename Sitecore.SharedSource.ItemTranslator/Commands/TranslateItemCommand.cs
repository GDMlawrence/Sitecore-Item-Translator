using Sitecore.Data;

namespace Sitecore.SharedSource.ItemTranslator.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Data.Fields;
    using Data.Items;
    using Diagnostics;
    using Globalization;
    using HtmlAgilityPack;
    using Jobs;
    using Shell.Applications.Dialogs.ProgressBoxes;
    using Shell.Framework.Commands;

    public class TranslateItemCommand : Command
    {
        protected const int MaxServiceRequestLength = 1000;

        protected string BaseLanguage
        {
            get
            {
                return Sitecore.Configuration.Settings.GetSetting("BaseLanguage", "en");  
            }
        }

        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            Item item = context.Items[0];
            ProgressBox.Execute("ItemSync", "Translate", this.GetIcon(context, string.Empty), TranslateItem, "item:load(id=" + item.ID + ")", new object[] { item, context });
        }

        public void TranslateItem(params object[] parameters)
        {
            var context = parameters[1] as CommandContext;
            if (context != null)
            {
                Item item = parameters[0] as Item;
                if (item != null)
                {
                    this.TranslateItem(item);
                }
            }
        }

        public virtual Item TranslateItem(Item item)
        {
            var translationService = GetTranslatorService(item);
             
            TranslateItem(item, translationService);
            return item;
        }

        protected ITranslationService GetTranslatorService(Item item)
        {

            return new AzureCogTranslationService(
                Sitecore.Configuration.Settings.GetSetting("Azure_CogService"),
                Sitecore.Configuration.Settings.GetSetting("Azure_SubscriptionKey"),
                BaseLanguage,
                item.Language.CultureInfo.TwoLetterISOLanguageName,
                double.Parse(Sitecore.Configuration.Settings.GetSetting("Azure_RequestTimeout")));
            
        }

        public void TranslateItem(Item item, ITranslationService service)
        {
            var database = Sitecore.Context.ContentDatabase;
            if (database == null)
                database = Database.GetDatabase("master");

            var sourceItem = database.GetItem(item.ID, Sitecore.Globalization.Language.Parse(BaseLanguage), Data.Version.Latest);
            if (sourceItem == null)
            {
                return;
            }

            Job job = Context.Job;
            if (job != null)
            {
                job.Status.LogInfo(Translate.Text("Translating item by path {0}.", new object[] { item.Paths.FullPath }));
            }

            item = GenerateVersion(item, job);

            item.Editing.BeginEdit();

            foreach (Field field in sourceItem.Fields)
            {
                if (string.IsNullOrEmpty(field.Name) || (string.IsNullOrEmpty(sourceItem[field.Name]) || field.Shared))
                {
                    continue;
                }

                if (!FieldIsTranslatable(field) || FieldIsStandard(field))
                {
                    item[field.Name] = sourceItem[field.Name];
                }
                else
                {
                    Sitecore.Diagnostics.Log.Info($"Translator->Field Type: {field.TypeKey}", "Translator");
                    if (field.TypeKey == "rich text")
                    {

                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(sourceItem[field.Name]);
                        HtmlNodeCollection coll = doc.DocumentNode.SelectNodes("//text()[normalize-space(.) != '']");

                        foreach (HtmlNode node in coll)
                        {
                            if (node.InnerText == node.InnerHtml)
                            {
                                node.InnerHtml = service.Translate(node.InnerText);
                            }
                        }
                        item[field.Name] = doc.DocumentNode.OuterHtml;
                    }
                    else
                    {
                        var text = sourceItem[field.Name];
                        var translatedText = string.Empty;

                        if (text.Length < MaxServiceRequestLength)
                        {
                            Sitecore.Diagnostics.Log.Info($"Translator->translating...{text} MaxServiceRequestLength: {MaxServiceRequestLength}", "Translator");
                            item[field.Name] = service.Translate(text);
                            continue;
                        }
                        translatedText = SplitText(text, MaxServiceRequestLength)
                            .Aggregate(translatedText, (current, textBlock) =>
                                current + service.Translate(textBlock)
                                );

                        item[field.Name] = translatedText;
                    }
                }
            }

            item.Editing.EndEdit();
        }

        private static bool FieldIsTranslatable(Field field)
        {
            return !((field.TypeKey == "image") ||
                        (field.TypeKey == "reference") ||
                        (field.TypeKey == "general link") ||
                        (field.TypeKey == "datetime") ||
                        (field.TypeKey == "droplink") ||
                        (field.TypeKey == "droplist") ||
                        (field.TypeKey == "treelist") ||
                        (field.TypeKey == "droptree") ||
                        (field.TypeKey == "multilist") ||
                        (field.TypeKey == "checklist") ||
                        (field.TypeKey == "treelistex") ||
                        (field.TypeKey == "checkbox"));
        }

        private static bool FieldIsStandard(Field field)
        {
            return field.Definition.Template.Name == "Advanced" ||
                        field.Definition.Template.Name == "Appearance" ||
                        field.Definition.Template.Name == "Help" ||
                        field.Definition.Template.Name == "Layout" ||
                        field.Definition.Template.Name == "Lifetime" ||
                        field.Definition.Template.Name == "Insert Options" ||
                        field.Definition.Template.Name == "Publishing" ||
                        field.Definition.Template.Name == "Security" ||
                        field.Definition.Template.Name == "Statistics" ||
                        field.Definition.Template.Name == "Tasks" ||
                        field.Definition.Template.Name == "Validators" ||
                        field.Definition.Template.Name == "Workflow";
        }

        private static IEnumerable<string> SplitText(string text, int numberOfSymbols)
        {
            int offset = 0;
            List<string> lines = new List<string>();
            while (offset < text.Length)
            {
                int index = text.LastIndexOf(" ", Math.Min(text.Length, offset + numberOfSymbols));
                string line = text.Substring(offset, (index - offset <= 0 ? text.Length : index) - offset);
                offset += line.Length + 1;
                Sitecore.Diagnostics.Log.Info($"Translator->splitText: {line}", "Translator");
                lines.Add(line);
            }

            return lines;
        }


        private Item GenerateVersion(Item item, Job job)
        {
            Item versionItem = null;
            var vOption = Sitecore.Configuration.Settings.GetSetting("VersionOption");
            job?.Status.LogInfo(Translate.Text("Language and Option {0} and {1}.", new object[] { item.Language, vOption }));
            if (!string.IsNullOrEmpty(vOption))
            {
                switch (vOption.ToUpper())
                {
                    case "REPLACELAST":
                        versionItem = ReplaceLastVersion(item, job);
                        break;
                    case "REPLACEALL":
                        versionItem = ReplaceAllVersions(item, job);
                        break;
                    default:
                        versionItem = item.Versions.AddVersion();
                        break;
                }
            }
            else
            {
                versionItem = item.Versions.AddVersion();
            }
            return versionItem;

        }
        private Item ReplaceLastVersion(Item item, Job job)
        {
            job?.Status.LogInfo(Translate.Text("ReplaceLastVersion {0}.", new object[] { item.Language }));
            return item.Versions.Count > 0 ? item.Versions.GetLatestVersion() : item.Versions.AddVersion();
        }

        private Item ReplaceAllVersions(Item item, Job job)
        {
            job?.Status.LogInfo(Translate.Text("ReplaceAllVersions {0}.", new object[] { item.Language }));
            if (item.Versions.Count > 0)
            {
                item.Versions.RemoveAll(false);
            }
            return item.Versions.AddVersion();
        }
    }
}
