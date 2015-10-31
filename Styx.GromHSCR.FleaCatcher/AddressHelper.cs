using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace Styx.GromHSCR.FleaCatcher
{
    public class AddressHelper
    {
        private const string BuildingRegex1 = "строение";
        private const string BuildingRegex2 = "стр.";
        private const string BuildingRegex3 = "ст.";
        private const string BuildingReplacement = "с.";

        private const string HousingRegex1 = "корпус.";
        private const string HousingRegex2 = "корп.";
        private const string HousingRegex3 = "кор.";
        private const string HousingReplacement = "к.";

        private const string StreetRegex1 = @"улица";
        private const string StreetReplacement1 = "ул";

        private const string StreetRegex2 = "бульвар";
        private const string StreetRegex3 = "бульв";
        private const string StreetRegex6 = "бул.";
        private const string StreetReplacement2 = "б-р";

        private const string StreetRegex4 = "проспект";
        private const string StreetRegex5 = "просп.";
        private const string StreetReplacement3 = "пр-т";


        private const string SlashRegex = @"/";
        private const string BackSlashRegex = @"\\";
        private const string SharpRegex = "#";
        private const string SlashReplacement = "-";

        private const string CharRegex1 = "й";
        private const string CharReplacement1 = "и";

        private const string CharRegex2 = "ё";
        private const string CharReplacement2 = "е";



        private const string LanguageCharRegex1 = @"c";
        private const string LanguageCharReplacement1 = "с";
        private const string LanguageCharRegex2 = @"C";
        private const string LanguageCharReplacement2 = "С";

        private const string LanguageCharRegex3 = @"e";
        private const string LanguageCharReplacement3 = "е";
        private const string LanguageCharRegex4 = @"E";
        private const string LanguageCharReplacement4 = "е";

        private const string LanguageCharRegex5 = @"a";
        private const string LanguageCharReplacement5 = "а";
        private const string LanguageCharRegex6 = @"A";
        private const string LanguageCharReplacement6 = "а";

        private const string LanguageCharRegex7 = @"p";
        private const string LanguageCharReplacement7 = "р";
        private const string LanguageCharRegex8 = @"P";
        private const string LanguageCharReplacement8 = "Р";

        private const string LanguageCharRegex9 = @"o";
        private const string LanguageCharReplacement9 = "о";
        private const string LanguageCharRegex10 = @"O";
        private const string LanguageCharReplacement10 = "о";

        private const string LanguageCharRegex11 = @"T";
        private const string LanguageCharReplacement11 = "Т";

        private const string LanguageCharRegex12 = @"B";
        private const string LanguageCharReplacement12 = "В";

        private const string LanguageCharRegex13 = @"K";
        private const string LanguageCharReplacement13 = "К";

        private const string LanguageCharRegex14 = @"X";
        private const string LanguageCharReplacement14 = "х";

        static Random _random = new Random();

        public string GetUnifiedAddress(string input)
        {
            var result = GetWritableAddress(input);
            result = Regex.Replace(result, CharRegex1, CharReplacement1, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, CharRegex2, CharReplacement2, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, "адрес", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, " тип", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, "расходометра", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, "расходомера", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, ":", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, "_", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, @"\.", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, ",", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, " ", "", RegexOptions.IgnoreCase);

            return result.ToLower();
        }

        public AddressFleas GetUnifiedAddresses(string input)
        {
            var resulter = GetWritableAddress(input);
            resulter = Regex.Replace(resulter, CharRegex1, CharReplacement1, RegexOptions.IgnoreCase);
            resulter = Regex.Replace(resulter, CharRegex2, CharReplacement2, RegexOptions.IgnoreCase);
            var stringResults = new AddressFleas() { Source = input, Variables = new List<string>() };
            var results = new List<string>();
            var resulter1 = resulter.Split(' ');
            var count = resulter1.Count();
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {

                    System.Random rnd = new System.Random();
                    var resulter3 = RandomizeStrings(resulter1);
                    var resulter2 = resulter3.Aggregate("", (current, next) => " " + current + " " + next + " ");
                    if (results.All(p => p != resulter2))
                        results.Add(resulter2);
                }
            }

            foreach (var result in results)
            {
                var result2 = Regex.Replace(result, "адрес", "", RegexOptions.IgnoreCase);
                result2 = Regex.Replace(result2, " тип ", "", RegexOptions.IgnoreCase);
                result2 = Regex.Replace(result2, "расходометра", "", RegexOptions.IgnoreCase);
                result2 = Regex.Replace(result2, "расходомера", "", RegexOptions.IgnoreCase);
                result2 = Regex.Replace(result2, ":", "", RegexOptions.IgnoreCase);
                result2 = Regex.Replace(result2, "_", "", RegexOptions.IgnoreCase);
                result2 = Regex.Replace(result2, @"\.", "", RegexOptions.IgnoreCase);
                result2 = Regex.Replace(result2, ",", "", RegexOptions.IgnoreCase);
                result2 = Regex.Replace(result2, " ", "", RegexOptions.IgnoreCase);
                result2 = result2.ToLower();
                stringResults.Variables.Add(result2);
            }

            return stringResults;
        }

        public string GetWritableAddress(string input)
        {
            var result = Regex.Replace(input, BuildingRegex1, BuildingReplacement, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, BuildingRegex2, BuildingReplacement, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, BuildingRegex3, BuildingReplacement, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, HousingRegex1, HousingReplacement, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, HousingRegex2, HousingReplacement, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, HousingRegex3, HousingReplacement, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, SlashRegex, SlashReplacement, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, BackSlashRegex, SlashReplacement, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, SharpRegex, SlashReplacement, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex1, LanguageCharReplacement1, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex2, LanguageCharReplacement2, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex3, LanguageCharReplacement3, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex4, LanguageCharReplacement4, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex5, LanguageCharReplacement5, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex6, LanguageCharReplacement6, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex7, LanguageCharReplacement7, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex8, LanguageCharReplacement8, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex9, LanguageCharReplacement9, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex10, LanguageCharReplacement10, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex11, LanguageCharReplacement11, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex12, LanguageCharReplacement12, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex13, LanguageCharReplacement13, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, LanguageCharRegex14, LanguageCharReplacement14, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, StreetRegex1, StreetReplacement1, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, StreetRegex2, StreetReplacement2, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, StreetRegex3, StreetReplacement2, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, StreetRegex6, StreetReplacement2, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, StreetRegex4, StreetReplacement3, RegexOptions.IgnoreCase);
            result = Regex.Replace(result, StreetRegex5, StreetReplacement3, RegexOptions.IgnoreCase);
            while (result.Contains("  "))
            {
                result.Replace("  ", " ");
            }
            result = Regex.Replace(result, ":", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, "_", "", RegexOptions.IgnoreCase);

            return result;
        }

        private List<SearchReplacePattern> searchReplaceAddressPatterns = new List<SearchReplacePattern>
        {
            new SearchReplacePattern(){
                SearchPattern = @"Адрес потребителя(.*)телефон",
                ReplacePatterns = new List<string>()
                {
                   "Адрес потребителя", "телефон"
                },
                Sort = 100},
                new SearchReplacePattern(){
                SearchPattern = @"Адрес потребителя(.*)ответственное",
                ReplacePatterns = new List<string>()
                {
                   "Адрес потребителя", "ответственное"
                },
                Sort = 200},
                 new SearchReplacePattern(){
                SearchPattern = @"Адрес потребителя(.*)вычислитель",
                ReplacePatterns = new List<string>()
                {
                   "Адрес потребителя", "вычислитель"
                },
                Sort = 300},
                 new SearchReplacePattern(){
                SearchPattern = @"Адрес объекта(.*)теплосчетчик",
                ReplacePatterns = new List<string>()
                {
                   "Адрес объекта", "теплосчетчик"
                },
                Sort = 400},
                 new SearchReplacePattern(){
                SearchPattern = @"Адрес дома(.*)телефон",
                ReplacePatterns = new List<string>()
                {
                   "Адрес дома", "телефон"
                },
                Sort = 500},
                 new SearchReplacePattern(){
                SearchPattern = @"Адрес дома(.*)дата",
                ReplacePatterns = new List<string>()
                {
                   "Адрес дома", "дата"
                },
                Sort = 600},
                 new SearchReplacePattern(){
                SearchPattern = @"Адрес(.*)телефон",
                ReplacePatterns = new List<string>()
                {
                   "Адрес", "телефон"
                },
                Sort = 700},
                 new SearchReplacePattern(){
                SearchPattern = @"Адрес(.*)ЦТП",
                ReplacePatterns = new List<string>()
                {
                   "Адрес", "ЦТП"
                },
                Sort = 800},
                new SearchReplacePattern(){
                SearchPattern = @"Потребитель(.*)тип",
                ReplacePatterns = new List<string>()
                {
                   "Потребитель", "тип"
                },
                Sort = 900},
        };

        //private const string searchAddressRegex = @"Адрес потребителя(.*)телефон";
        //private const string searchAddressRegex2 = @"Адрес дома(.*)телефон";
        //private const string searchAddressRegex3 = @"Адрес дома(.*)Дата";
        //private const string searchAddressRegex4 = @"Потребитель(.*)тип";
        //private const string searchAddressRegex5 = @"Адрес(.*)телефон";
        //private const string searchAddressRegex6 = @"Адрес объекта(.*)теплосчетчик";
        //private const string searchAddressRegex7 = @"Адрес (.*)ЦТП";
        //private const string searchAddressRegex8 = @"Адрес потребителя(.*)ответственное";
        //private const string searchAddressRegex9 = @"Адрес потребителя(.*)вычислитель";

        //private const string searchAddressReplaceRegex = "Адрес потребителя";
        //private const string searchAddressReplaceRegex1 = "телефон";
        //private const string searchAddressReplaceRegex2 = "Адрес дома";
        //private const string searchAddressReplaceRegex3 = "Дата";
        //private const string searchAddressReplaceRegex4 = "Потребитель";
        //private const string searchAddressReplaceRegex5 = "Адрес";
        //private const string searchAddressReplaceRegex6 = "Тип";
        //private const string searchAddressReplaceRegex7 = "Адрес объекта";
        //private const string searchAddressReplaceRegex8 = "теплосчетчик";
        //private const string searchAddressReplaceRegex9 = "цтп";

        public string SearchAddress(string input)
        {
            var patterns = searchReplaceAddressPatterns.OrderBy(p => p.Sort).ToList();
            foreach (var searchReplacePattern in patterns)
            {
                if (!Regex.IsMatch(input, searchReplacePattern.SearchPattern, RegexOptions.IgnoreCase)) continue;
                var address = Regex.Match(input, searchReplacePattern.SearchPattern, RegexOptions.IgnoreCase).Value;
                address = searchReplacePattern.ReplacePatterns.Aggregate(address, (current, replacePattern) => Regex.Replace(current, replacePattern, "", RegexOptions.IgnoreCase));
                return address.Trim();
            }
            return input;
        }

        private List<SearchReplacePattern> searchReplaceSystemPatterns = new List<SearchReplacePattern>()
        {
            new SearchReplacePattern()
            {
                SearchPattern = @"Система(.*)Посуточная",
                ReplacePatterns = new List<string>(){"Система","Посуточная"},
                Sort = 100
            },
            new SearchReplacePattern()
            {
                SearchPattern = @"Т/С(.*)нарастающим",
                ReplacePatterns = new List<string>(){"Т/С","нарастающим"},
                Sort = 200
            },
            new SearchReplacePattern()
            {
                SearchPattern = @"Система(.*)месячный",
                ReplacePatterns = new List<string>(){"Система","месячный"},
                Sort = 300
            },
            new SearchReplacePattern()
            {
                SearchPattern = @"Система(.*)месячный",
                ReplacePatterns = new List<string>(){"Система","месячный"},
                Sort = 300
            },
        };

        private const string searchSystemRegex3 = "ГВС";
        private const string searchSystemRegex4 = "ЦО";
        private const string searchSystemRegex5 = "Отопление";

        //private const string searchSystemRegex = @"Система(.*)Посуточная";
        //private const string searchSystemRegex2 = @"Т/С(.*)нарастающим";
        //private const string searchSystemRegex6 = @"Система(.*)месячный";


        //private const string searchSystemReplaceRegex = @"Система";
        //private const string searchSystemReplaceRegex2 = @"Посуточная";
        //private const string searchSystemReplaceRegex3 = @"Т/С";
        //private const string searchSystemReplaceRegex4 = @"нарастающим";
        //private const string searchSystemReplaceRegex5 = @"месячный";


        public string SearchSystem(string input)
        {
            foreach (var searchReplaceSystemPattern in searchReplaceSystemPatterns)
            {
                if (!Regex.IsMatch(input, searchReplaceSystemPattern.SearchPattern, RegexOptions.IgnoreCase)) continue;
                var system = Regex.Match(input, searchReplaceSystemPattern.SearchPattern, RegexOptions.IgnoreCase).Value;
                system = searchReplaceSystemPattern.ReplacePatterns.Aggregate(system, (current, replacePattern) => Regex.Replace(current, replacePattern, "", RegexOptions.IgnoreCase));
                return system.Trim();
            }
            if (Regex.IsMatch(input, searchSystemRegex3, RegexOptions.IgnoreCase))
                return "ГВС";
            if (Regex.IsMatch(input, searchSystemRegex4, RegexOptions.IgnoreCase))
                return "ЦО";
            if (Regex.IsMatch(input, searchSystemRegex5, RegexOptions.IgnoreCase))
                return "ЦО";
            return "";
        }
        public static string[] RandomizeStrings(string[] arr)
        {
            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
            // Add all strings from array
            // Add new random int each time
            foreach (string s in arr)
            {
                list.Add(new KeyValuePair<int, string>(_random.Next(), s));
            }
            // Sort the list by the random number
            var sorted = from item in list
                         orderby item.Key
                         select item;
            // Allocate new string array
            string[] result = new string[arr.Length];
            // Copy values to array
            int index = 0;
            foreach (KeyValuePair<int, string> pair in sorted)
            {
                result[index] = pair.Value;
                index++;
            }
            // Return copied array
            return result;
        }
    }


    public class AddressFleas
    {
        public string Source { get; set; }

        public List<string> Variables { get; set; }

    }

    public class SearchReplacePattern
    {
        public string SearchPattern { get; set; }

        public List<string> ReplacePatterns { get; set; }

        public int Sort { get; set; }

    }
}
