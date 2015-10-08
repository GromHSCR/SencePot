namespace SenceRep.GromHSCR.Validations.Attributes
{
	public interface IUniquePropertyChecker
	{
		bool IsPropertyUnique(string propertyName);
	}
}