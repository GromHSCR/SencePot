using System;
using System.Reflection;
using SenceRep.GromHSCR.CompostionBase.Configurations;

namespace SenceRep.GromHSCR.CompostionBase
{
	public static class Composition
	{
		private static CompositionContainer _container;
		private static AggregateCatalog _catalog;



		public static void RegisterCatalogsFromConfig()
		{
			var configSection = CompositionConfigurationSection.GetDefault();
			foreach (CompositionCatalogConfigurationElement catalogDecsription in configSection.Catalogs)
			{
				RegisterCatalog(catalogDecsription.Catalog);
			}
		}

		public static void RegisterCatalog(ComposablePartCatalog catalog)
		{
			if (catalog == null) throw new ArgumentNullException("catalog");

			if (_container != null)
				throw new InvalidOperationException("RegisterCatalog method have to be called before first use of Container");

			if (_catalog == null)
				_catalog = new AggregateCatalog();

			if (!_catalog.Catalogs.Contains(catalog))
				_catalog.Catalogs.Add(catalog);
		}

		public static CompositionContainer Container
		{
			get
			{
				if (_container == null)
				{
					if (_catalog == null)
						RegisterCatalog(new AssemblyCatalog(Assembly.GetCallingAssembly()));

					_container = new CompositionContainer(_catalog);
				}

				return _container;
			}
		}

		public static T ComposeParts<T>(T attributedPart) where T: class
		{
			if (attributedPart == null) return null;

			var batchToAdd = new CompositionBatch();
			var composablePart = batchToAdd.AddPart(attributedPart);
			Container.Compose(batchToAdd);

			var batchToRemove = new CompositionBatch();
			batchToRemove.RemovePart(composablePart);
			Container.Compose(batchToRemove);

			return attributedPart;
		}

		public static T GetExportedValue<T>()
		{
			return Container.GetExportedValue<T>();
		}

		public static T Resolve<T>()
		{
			return Container.GetExportedValue<T>();
		}
	}
}
