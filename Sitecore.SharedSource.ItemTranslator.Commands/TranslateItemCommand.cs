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

namespace Sitecore.SharedSource.ItemTranslator.Commands
{
	internal class TranslateItemCommand : Command
	{
		protected const string SourceLanguage = "en";

		protected const int MaxServiceRequestLength = 1000;

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
			CommandContext commandContext = parameters[1] as CommandContext;
			if (commandContext != null)
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
			ITranslationService translatorService = this.GetTranslatorService(item);
			this.TranslateItem(item, translatorService);
			return item;
		}

		protected ITranslationService GetTranslatorService(Item item)
		{
			string setting = Settings.GetSetting("TranslationProvider");
			ITranslationService result;
			if (setting != null)
			{
				if (setting == "Google")
				{
					result = new GoogleTranslateService("en", item.Language.CultureInfo.TwoLetterISOLanguageName);
					return result;
				}
				if (setting == "Bing")
				{
					result = new BingTranslateService("en", item.Language.CultureInfo.TwoLetterISOLanguageName, Settings.GetSetting("BingApplicationId"));
					return result;
				}
			}
			result = new GoogleTranslateService("en", item.Language.CultureInfo.TwoLetterISOLanguageName);
			return result;
		}

		public void TranslateItem(Item item, ITranslationService service)
		{
			Item item2 = Context.ContentDatabase.GetItem(item.ID, Language.Parse("en"));
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
				if (item2 != null)
				{
					item = item.Versions.AddVersion();
					item.Editing.BeginEdit();
					foreach (Field field in item2.Fields)
					{
						if (!string.IsNullOrEmpty(item2[field.Name]) && !field.Shared)
						{
							if (!this.FieldIsTranslatable(field) || this.FieldIsStandard(field))
							{
								item[field.Name] = item2[field.Name];
							}
							else
							{
								string text = item2[field.Name];
								string text2 = string.Empty;
								if (text.Length < 1000)
								{
									item[field.Name] = service.Translate(text);
								}
								else
								{
									foreach (string current in this.SplitText(text, 1000))
									{
										text2 += service.Translate(current);
									}
									item[field.Name] = text2;
								}
							}
						}
					}
					item.Editing.EndEdit();
				}
			}
		}

		private bool FieldIsTranslatable(Field field)
		{
			return !(field.TypeKey == "image") && !(field.TypeKey == "reference") && !(field.TypeKey == "general link") && !(field.TypeKey == "datetime") && !(field.TypeKey == "droplink") && !(field.TypeKey == "droplist") && !(field.TypeKey == "treelist") && !(field.TypeKey == "droptree") && !(field.TypeKey == "multilist") && !(field.TypeKey == "checklist") && !(field.TypeKey == "treelistex") && !(field.TypeKey == "checkbox");
		}

		private bool FieldIsStandard(Field field)
		{
			return field.Definition.Template.Name == "Advanced" || field.Definition.Template.Name == "Appearance" || field.Definition.Template.Name == "Help" || field.Definition.Template.Name == "Layout" || field.Definition.Template.Name == "Lifetime" || field.Definition.Template.Name == "Insert Options" || field.Definition.Template.Name == "Publishing" || field.Definition.Template.Name == "Security" || field.Definition.Template.Name == "Statistics" || field.Definition.Template.Name == "Tasks" || field.Definition.Template.Name == "Validators" || field.Definition.Template.Name == "Workflow";
		}

		private IEnumerable<string> SplitText(string text, int numberOfSymbols)
		{
			int i = 0;
			List<string> list = new List<string>();
			while (i < text.Length)
			{
				int num = text.LastIndexOf(" ", Math.Min(text.Length, i + numberOfSymbols));
				string text2 = text.Substring(i, ((num - i <= 0) ? text.Length : num) - i);
				i += text2.Length + 1;
				list.Add(text2);
			}
			return list;
		}
	}
}
