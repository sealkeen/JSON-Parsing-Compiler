using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using EPAM.CSCourse2016.JSONParser.Library;
using System.Collections.Generic;

namespace QueriesClient
{
    public class Program
    {
        private static Random rnd = new Random();
        private const string _name = "-name";
        private const string _minmark = "-minmark";
        private const string _maxmark = "-maxmark";
        private const string _datefrom = "-datefrom";
        private const string _dateto = "-dateto";
        private const string _test = "-test";

        static public void Main(string[] args)
        {
            var filePath = FindJSONFile();
            JSONParser jSONParser;
            if (filePath == null)
                jSONParser = new JSONParser(new string[] { "\"Math Test\":[{{\"Ivan\":\"Ivanov\"},\"9 / 26 / 2021 6:27:19 PM\",4},{{\"Pyotr\":\"Petrov\"},\"9 / 26 / 2021 6:27:19 PM\",3},{{\"Vasily\":\"Vasilyev\"},\"9 / 26 / 2021 6:27:19 PM\",3},{{\"Maria\":\"Marieva\"},\"9 / 26 / 2021 6:27:19 PM\",4},{{\"Pavel\":\"Pavlov\"},\"9 / 26 / 2021 6:27:19 PM\",4},{{\"Roman\":\"Romanov\"},\"9 / 26 / 2021 6:27:19 PM\",4},{{\"Boris\":\"Borisov\"},\"9 / 26 / 2021 6:27:19 PM\",3},{{\"Ulya\":\"Ulyeva\"},\"9 / 26 / 2021 6:27:19 PM\",2}]" });
            else
                jSONParser = new JSONParser(filePath);
            var jItem = jSONParser.Parse();

            var jItemList = new List<JItem>();
            jItem.ListAllNodes(ref jItemList);

            var query = from JItem item in jItemList where item.HasKeyOrValue() select (JKeyValuePair)item;
            
            QueryRetriver queryRetriver = new QueryRetriver();
            queryRetriver.ShowQuery(query);

            args = new string[] { "-mark", "3" };
            queryRetriver.ShowQueryEqualByParameter(query, args, "-mark", false, JItemType.SingleValue);

            args = new string[] { "-mark", "3" };
            queryRetriver.ShowQueryWithIntComparedToParameter(query,
                args, "-mark", CompareType.LessOrEquals, false, JItemType.SingleValue);

            args = new string[] { "-mark", "4" };
            queryRetriver.ShowQueryWithIntComparedToParameter(query,
                args, "-mark", CompareType.MoreOrEquals, false, JItemType.SingleValue);

            args = new string[] { "-name", "Ivan" };
            queryRetriver.ShowQueryEqualByParameter(query, args, "-name", true, JItemType.String);

            args = new string[] { "-name", "Ivan" };
            queryRetriver.ShowQueryEqualByParameter(query, args, "-name", true, JItemType.String);

            args = new string[] { "-date", "11/20/2012" };
            queryRetriver.ShowQueryWithDateTimeComparedToParameter(query, args, "-date", CompareType.MoreOrEquals,
                false, JItemType.String);

            Console.Read();
        }

        private static string FindJSONFile()
        {
            string result = "";
            DirectoryInfo dI = new DirectoryInfo(Environment.CurrentDirectory);
            while (dI.Name != "M09" && dI.Parent.Exists)
            {
                dI = dI.Parent;
            }
            result = $@"{dI.FullName}\QueriesTests\bin\Release\netcoreapp3.1\MathTest.json";
            if (File.Exists(result))
                return result;
            else
                return null;
        }
    }
}
