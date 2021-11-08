using System;

namespace EPAM.CSCourse2016.ParserPerfTester.Common
{
    public interface IParser
    {
        string ToTestString(string json);
    }
}
