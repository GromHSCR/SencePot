namespace Styx.GromHSCR.Helpers
{
	public static class StringHelper
	{
		/// <summary>
		/// Склоняет существительное после числительного
		/// </summary>
		/// <param name="number">числительное</param>
		/// <param name="nominative">Именительный падеж, например: документ</param>
		/// <param name="genitiveSingular">Родительный падеж, единственное число, например: документа</param>
		/// <param name="genitivePlural">Родительный падеж, множественное число, например: документов</param>
		/// <returns></returns>
		public static string Declension(int number, string nominative, string genitiveSingular, string genitivePlural)
		{
			var last = number % 10;
			var last2 = number % 100;
			if (last == 1 && last2 != 11)
				return nominative;
			return last == 2 && last2 != 12 || last == 3 && last2 != 13 || last == 4 && last2 != 14 ? genitiveSingular : genitivePlural;
		}
	}
}
