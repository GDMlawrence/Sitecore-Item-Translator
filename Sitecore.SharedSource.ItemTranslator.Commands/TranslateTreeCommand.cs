using Sitecore.Data.Items;
using System;

namespace Sitecore.SharedSource.ItemTranslator.Commands
{
	internal class TranslateTreeCommand : TranslateItemCommand
	{
		protected override Item TranslateItem(Item item)
		{
			ITranslationService translatorService = base.GetTranslatorService(item);
			base.TranslateItem(item, translatorService);
			Item[] descendants = item.Axes.GetDescendants();
			Item[] array = descendants;
			for (int i = 0; i < array.Length; i++)
			{
				Item item2 = array[i];
				base.TranslateItem(item2, translatorService);
			}
			return item;
		}
	}
}
