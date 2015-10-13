namespace SenceRep.Validations.Attributes
{
	public interface IUniquePropertyChecker
	{
		bool IsPropertyUnique(string propertyName);
	}
}