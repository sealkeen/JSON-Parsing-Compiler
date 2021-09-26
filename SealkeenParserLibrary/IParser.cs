using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.CSCourse2016.ParserPerfTester.Common
{
    public interface IParser
    {
        string ToTestString(string json);
    }
}
