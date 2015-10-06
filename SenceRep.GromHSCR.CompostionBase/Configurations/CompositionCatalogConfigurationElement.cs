using System;

namespace SenceRep.GromHSCR.CompostionBase.Configurations
{
	public class CompositionCatalogConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		public string CatalogName
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("type", IsRequired = true)]
		public string CatalogTypeName
		{
			get { return (string)this["type"]; }
			set { this["type"] = value; }
		}

		public Type CatalogType
		{
			get
			{
				var type = Type.GetType(this.CatalogTypeName);

				if (type == null)
					throw new InvalidProgramException(String.Format("Invalid catalog type {0}", this.CatalogTypeName));

				return type;
			}
		}

		public ComposablePartCatalog Catalog
		{
			get
			{
				var catalog = Activator.CreateInstance(this.CatalogType) as ComposablePartCatalog;
				if (catalog == null)
					throw new InvalidCastException("Catalog type should be inherited from ComposablePartCatalog class");
				return catalog;
			}
		}
	}
}
