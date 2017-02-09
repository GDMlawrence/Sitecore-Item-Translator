using HtmlAgilityPack;
using Sitecore.Configuration;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Jobs;
using Sitecore.Shell.Applications.Dialogs.ProgressBoxes;
using Sitecore.Shell.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.SharedSource.ItemTranslator.Commands
{
	internal class TranslateItemCommand : Command
	{
		protected const int MaxServiceRequestLength = 1000;

		protected string BaseLanguage
		{
			get
			{
				return Settings.GetSetting("BaseLanguage", "en");
			}
		}

		public override void Execute(CommandContext context)
		{
			Assert.ArgumentNotNull(context, "context");
			Item item = context.Items[0];
			ProgressBox.Execute("ItemSync", "Translate", this.GetIcon(context, string.Empty), new ProgressBoxMethod(this.TranslateItem), "item:load(id=" + item.ID + ")", new object[]
			{
				item,
				context
			});
		}

		private void TranslateItem(params object[] parameters)
		{
			CommandContext context = parameters[1] as CommandContext;
			if (context != null)
			{
				Item item = parameters[0] as Item;
				if (item != null)
				{
					this.TranslateItem(item);
				}
			}
		}

		protected virtual Item TranslateItem(Item item)
		{
			ITranslationService translationService = this.GetTranslatorService(item);
			this.TranslateItem(item, translationService);
			return item;
		}

		protected ITranslationService GetTranslatorService(Item item)
		{
            switch (Sitecore.Configuration.Settings.GetSetting("TranslationProvider"))
            {
                case "MSTranslation":
                    {
                        return new MSTranslationService(BaseLanguage,
                            item.Language.CultureInfo.TwoLetterISOLanguageName,
                            Sitecore.Configuration.Settings.GetSetting("MSTranslation_ClientID"),
                            Sitecore.Configuration.Settings.GetSetting("MSTranslation_ClientSecret"));
                    }
                case "AzureCogService":
                    {
                        return new AzureTranslationService(BaseLanguage,
                            item.Language.CultureInfo.TwoLetterISOLanguageName,
                            Sitecore.Configuration.Settings.GetSetting("Azure_SubscriptionKey"));
                    }
                default:
                    {
                        return new GoogleTranslateService(BaseLanguage,
                                                          item.Language.CultureInfo.TwoLetterISOLanguageName);
                    }
            }
        }

		public void TranslateItem(Item item, ITranslationService service)
		{
			Item sourceItem = Context.ContentDatabase.GetItem(item.ID, Language.Parse(this.BaseLanguage));
			Job job = Context.Job;
			if (job != null)
			{
				job.Status.LogInfo(Translate.Text("Translating item by path {0}.", new object[]
				{
					item.Paths.FullPath
				}));
			}
			if (item.Versions.Count == 0)
			{
				if (sourceItem == null)
				{
					return;
				}
				item = item.Versions.AddVersion();
				item.Editing.BeginEdit();
				foreach (Field field in sourceItem.Fields)
				{
					if (!string.IsNullOrEmpty(field.Name) && !string.IsNullOrEmpty(sourceItem[field.Name]) && !field.Shared)
					{
						if (!TranslateItemCommand.FieldIsTranslatable(field) || TranslateItemCommand.FieldIsStandard(field))
						{
							item[field.Name] = sourceItem[field.Name];
						}
						else if (field.TypeKey == "rich text")
						{
							Log.Info("HTML Field", "Translator");
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
							string text = sourceItem[field.Name];
							string translatedText = string.Empty;
							if (text.Length < 1000)
							{
								item[field.Name] = service.Translate(text);
							}
							else
							{
								translatedText = TranslateItemCommand.SplitText(text, 1000).Aggregate(translatedText, (string current, string textBlock) => current + service.Translate(textBlock));
								item[field.Name] = translatedText;
							}
						}
					}
				}
				item.Editing.EndEdit();
			}
		}

		private static bool FieldIsTranslatable(Field field)
		{
			return !(field.TypeKey == "image") && !(field.TypeKey == "reference") && !(field.TypeKey == "general link") && !(field.TypeKey == "datetime") && !(field.TypeKey == "droplink") && !(field.TypeKey == "droplist") && !(field.TypeKey == "treelist") && !(field.TypeKey == "droptree") && !(field.TypeKey == "multilist") && !(field.TypeKey == "checklist") && !(field.TypeKey == "treelistex") && !(field.TypeKey == "checkbox");
		}

		private static bool FieldIsStandard(Field field)
		{
			return field.Definition.Template.Name == "Advanced" || field.Definition.Template.Name == "Appearance" || field.Definition.Template.Name == "Help" || field.Definition.Template.Name == "Layout" || field.Definition.Template.Name == "Lifetime" || field.Definition.Template.Name == "Insert Options" || field.Definition.Template.Name == "Publishing" || field.Definition.Template.Name == "Security" || field.Definition.Template.Name == "Statistics" || field.Definition.Template.Name == "Tasks" || field.Definition.Template.Name == "Validators" || field.Definition.Template.Name == "Workflow";
		}

		private static System.Collections.Generic.IEnumerable<string> SplitText(string text, int numberOfSymbols)
		{
			int offset = 0;
			System.Collections.Generic.List<string> lines = new System.Collections.Generic.List<string>();
			while (offset < text.Length)
			{
				int index = text.LastIndexOf(" ", System.Math.Min(text.Length, offset + numberOfSymbols));
				string line = text.Substring(offset, ((index - offset <= 0) ? text.Length : index) - offset);
				offset += line.Length + 1;
				lines.Add(line);
			}
			return lines;
		}
	}
}
