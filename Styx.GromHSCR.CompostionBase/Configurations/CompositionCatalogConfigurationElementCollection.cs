using System.Configuration;

namespace Styx.GromHSCR.CompostionBase.Configurations
{
	public class CompositionCatalogConfigurationElementCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new CompositionCatalogConfigurationElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			var compositionCatalogConfigurationElement = element as CompositionCatalogConfigurationElement;
			return compositionCatalogConfigurationElement == null ? string.Empty : compositionCatalogConfigurationElement.CatalogName;
		}
	}
}