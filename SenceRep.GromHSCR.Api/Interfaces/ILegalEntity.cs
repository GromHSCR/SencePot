﻿using System;

namespace SenceRep.GromHSCR.Api.Interfaces
{
	interface ILegalEntity : IBaseItem
	{
		string Name { get; set; }

		Guid OrganizationId { get; set; }

		IOrganization Organization { get; set; }

		string INN { get; set; }
	}
}
