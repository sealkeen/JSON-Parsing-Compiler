using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPAM.CSCourse2016.;
using System.IO;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class JSONParserTester
    {
        private static Random rnd = new Random();
        [TestMethod]
        public void CreateJSONTest()
        {
            JRoot root = new JRoot();
            JKeyValuePair mathTest = new JKeyValuePair(new JString("Math Test"), null, root);
            JArray testResultArray = new JArray(mathTest);

            testResultArray.Add(
                CreateNewStudentTestResult(testResultArray, ("Ivan"), ("Ivanov")),
                CreateNewStudentTestResult(testResultArray, ("Pyotr"), ("Petrov")),
                CreateNewStudentTestResult(testResultArray, ("Vasily"), ("Vasilyev")),
                CreateNewStudentTestResult(testResultArray, ("Maria"), ("Marieva")),
                CreateNewStudentTestResult(testResultArray, ("Pavel"), ("Pavlov")),
                CreateNewStudentTestResult(testResultArray, ("Roman"), ("Romanov")),
                CreateNewStudentTestResult(testResultArray, ("Boris"), ("Borisov")),
                CreateNewStudentTestResult(testResultArray, ("Ulya"), ("Ulyeva"))
                );

            mathTest.Value = testResultArray;
            
            root.Add(mathTest);
            root.ToFile("MathTest.json");
            Process.Start("MathTest.json");
            Assert.IsTrue(File.Exists("MathTest.json"));
        }

        public JObject CreateNewStudentTestResult(JArray studentArray, string firstName, string lastName)
        {
            JObject testResult = new JObject(studentArray);
            JObject student = new JObject(testResult, new JKeyValuePair(new JString(firstName), new JString(lastName)));
            testResult.Add(student, new JString(DateTime.Now.ToString()), new JSingleValue(rnd.Next(2, 5).ToString()));
            return testResult;
        }
    }
}
