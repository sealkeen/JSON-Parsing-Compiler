using System;
using System.Collections.Generic;
using System.Linq;
using Sealkeen.CSCourse2016.JSONParser.Core;

namespace QueriesClient
{
    public class QueryRetriver
    {
        public void ShowQuery(IEnumerable<JItem> query)
        {
            foreach (var item in query)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("______");
        }

        public bool CompareByMethod<T>(T source, T target, CompareType compareType) where T : IComparable
        {
            if (source is null)
                return false;
            int compared = source.CompareTo(target);
            if (compared == 0)
                return true;
            else if (compared < 0 && compareType == CompareType.LessOrEquals)
            {
                return true;
            }
            else if (compared > 0 && compareType == CompareType.MoreOrEquals)
            {
                return true;
            }
            return false;
        }

        public void ShowQueryWithIntComparedToParameter(
        IEnumerable<JKeyValuePair> query,
        string[] args, string parameter, CompareType compareType,
        bool key = true,
        JItemType itemType = JItemType.SingleValue)
        {
            if (args.Contains(parameter))
            {
                if (args.Length > (Array.IndexOf(args, parameter) + 1))
                {
                    Console.WriteLine($"Queries with value {compareType} {args[Array.IndexOf(args, parameter) + 1]}");
                    var higherMarks = from JKeyValuePair pair in query
                                      where pair.ContainsIntegerValue()
                                      select pair;

                    var higherMarksObjects = higherMarks
                        .Where(p =>
                            CompareByMethod(
                            p.GetIntegerValue(),
                            int.Parse(args[Array.IndexOf(args, parameter) + 1]),
                            compareType)
                        )
                        .Select(x => x.FindContainerOrReturnParent(new JString("Student")));
                    ShowQuery(higherMarksObjects);
                }
            }
        }

        public void ShowQueryWithDateTimeComparedToParameter(
            IEnumerable<JKeyValuePair> query,
            string[] args, string parameter, CompareType compareType,
            bool key = true,
            JItemType itemType = JItemType.String)
        {
            if (args.Contains(parameter))
            {
                if (args.Length > (Array.IndexOf(args, parameter) + 1))
                {
                    Console.WriteLine($"Queries with date {compareType} {args[Array.IndexOf(args, parameter) + 1]}");
                    var higherMarks = from JKeyValuePair pair in query
                                      where pair.ContainsDateTimeValue()
                                      select pair;

                    var higherMarksObjects = higherMarks
                        .Where(p =>
                            CompareByMethod(
                            p.GetDateTimeValue(),
                            DateTime.Parse(args[Array.IndexOf(args, parameter) + 1]),
                            compareType)
                        )
                        .Select(x => x.FindContainerOrReturnParent(new JString("Student")));
                    ShowQuery(higherMarksObjects);
                }
            }
        }

        public void ShowQueryEqualByParameter(
        IEnumerable<JKeyValuePair> query,
        string[] args, string parameter, bool key = true,
        JItemType itemType = JItemType.SingleValue)
        {
            if (args.Contains(parameter))
            {
                if (args.Length > (Array.IndexOf(args, parameter) + 1))
                {
                    Console.WriteLine($"Queries with value Equal to {args[Array.IndexOf(args, parameter) + 1]}");
                    var student = new JString("Student");
                    var certainQuery = query.Where(x =>
                        (key ? x.Key : x.Value)
                        .Equals(JItem.Factory(itemType, args[Array.IndexOf(args, parameter) + 1]))
                        ).Select(x => x.FindContainerOrReturnParent(student));
                    ShowQuery(certainQuery);
                }
            }
        }
    }
}
