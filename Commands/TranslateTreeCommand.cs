using Sitecore.Data.Items;
using System;

namespace Sitecore.SharedSource.ItemTranslator.Commands
{
	internal class TranslateTreeCommand : TranslateItemCommand
	{
		protected override Item TranslateItem(Item item)
		{
			ITranslationService translationService = base.GetTranslatorService(item);
			base.TranslateItem(item, translationService);
			Item[] items = item.Axes.GetDescendants();
			Item[] array = items;
			for (int i = 0; i < array.Length; i++)
			{
				Item childItem = array[i];
				base.TranslateItem(childItem, translationService);
			}
			return item;
		}
	}
}
