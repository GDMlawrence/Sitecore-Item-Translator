# Sitecore Item Translator

Sitecore 8+

Updated to use Azure Cognitive Services

Includes ```Sitecore-item-translator.xml``` for generating sitecore package for distribution.

### Configuration

Update **Azure_SubscriptionKey** in ```Sitecore.SharedSource.ItemTranslator.config``` with [Azure Key](http://docs.microsofttranslator.com/text-translate.html).

Add the following to the Web.config

```
  <system.serviceModel>
    <bindings>
		  <basicHttpBinding>
			<binding name="BasicHttpBinding_LanguageService" />
		</basicHttpBinding>
    </bindings>
    <client>
		<endpoint address="http://api.microsofttranslator.com/V2/soap.svc"
			binding="basicHttpBinding" bindingConfiguration="BasicHttpBinding_LanguageService"
			contract="AzureCogService.LanguageService" name="BasicHttpBinding_LanguageService" />
    </client>
  </system.serviceModel>
```