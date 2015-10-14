using System;

namespace Styx.GromHSCR.MvvmBase.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public class AffectIsModifiedAttribute : Attribute
	{
		public bool AffectIsModified { get; private set; }

		public AffectIsModifiedAttribute(bool isOn = true)
		{
			AffectIsModified = isOn;
		}
	}
}