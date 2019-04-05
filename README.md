# Sitecore Item Translator

Sitecore 8+

Updated to use Azure Cognitive Services

Includes ```Sitecore-item-translator.xml``` for generating sitecore package for distribution.

### Configuration

Update **Azure_SubscriptionKey** in ```Sitecore.SharedSource.ItemTranslator.config``` with [Azure Key](http://docs.microsofttranslator.com/text-translate.html).

Add the following to the Web.config

```
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
  <sitecore>
    <settings>
      <setting name="TranslationProvider" value="AzureCogService"/>    
      <setting name="BaseLanguage" value="en"/>


      <setting name="Azure_CogService" value="https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&amp;from={0}&amp;to={1}"/>
      <setting name="Azure_SubscriptionKey" value="Subsciption Key from Azure"/>
      <!-- Request timeout in milliseconds -->
      <setting name="Azure_RequestTimeout" value="6000"/>

      <!-- 	New: Creates new version for language
			ReplaceLast: replaces last version if exists, or creates new version
			ReplaceAll: replaces all versions if exists, or creates new version -->
      <setting name="VersionOption" value="ReplaceAll" />
    </settings>
  </sitecore>
</configuration>

```