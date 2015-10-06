﻿using System;

namespace SenceRep.GromHSCR.CompostionBase.Configurations
{
	public class CompositionConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("catalogs", IsDefaultCollection = false)]
		public CompositionCatalogConfigurationElementCollection Catalogs
		{
			get { return (CompositionCatalogConfigurationElementCollection)this["catalogs"]; }
			set { this["catalogs"] = value; }
		}

		public static CompositionConfigurationSection GetDefault()
		{
			var section = ConfigurationManager.GetSection("mef.composition") as CompositionConfigurationSection;

			if (section == null)
				throw new InvalidProgramException("Configuration section for mef.composition not found");

			return section;
		}
	}
}