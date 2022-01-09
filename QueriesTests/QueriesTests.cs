using NUnit.Framework;
using System;
using EPAM.CSCourse2016.JSONParser.Library;
using System.Diagnostics;
using System.IO;
using QueriesClient;

namespace QueriesTests
{
    public class QueriesTests : ConsoleWriteLinable
    {
        [Test]
        public void ParseJSON()
        {
            QueriesClient.Program.Main(new string[] { "-mark", "3", "-name", "Ivan", "-date", "11/20/2012" });
        }

        [Test]
        public void CreateAndOpenJObjectFile()
        {
            JObject jObject = new JObject(null);
            jObject.Add(new JKeyValuePair(new JString("Key"), new JString("Value")));
            jObject.SaveToFileAndOpenInNotepad("jKeyValuePair.txt");
            // jObject.ToFile("filename.txt");
        }

        [Test]
        public void ParseTest()
        {
            JSONParser jSONParser = new JSONParser("jKeyValuePair.txt");
            var jItem = jSONParser.Parse();
            Debug.WriteLine(jItem.ToString());
        }

        private static Random rnd = new Random();
        [Test]
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
                CreateNewStudentTestResult(testResultArray, ("Ulya"), ("Ulyeva")),
                CreateNewStudentTestResult(testResultArray, ("Danil"), ("Danilov")),
                CreateNewStudentTestResult(testResultArray, ("Nikolay"), ("Nikolaev")),
                CreateNewStudentTestResult(testResultArray, ("Bela"), ("Belova")),
                CreateNewStudentTestResult(testResultArray, ("Grigory"), ("Gregoriev"))
                );

            mathTest.Value = testResultArray;

            root.Add(mathTest);
            bool saved = root.ToFile("MathTest.json");
            if (File.Exists("MathTest.json"))
                Process.Start("notepad.exe", "MathTest.json");
            Assert.IsTrue(File.Exists("MathTest.json"));
        }

        public JObject CreateNewStudentTestResult(JArray studentArray, string firstName, string lastName)
        {
            JObject testResult = new JObject(studentArray);
            JKeyValuePair student = new JKeyValuePair( 
                new JString( "Student" ),
                new JKeyValuePair(new JString(firstName), new JString(lastName))
                );
            testResult.Add( student, 
                new JKeyValuePair( new JString("date" ), 
                new JString( (DateTime.Now - TimeSpan.FromDays(rnd.Next(10))).ToString()) ), 
                new JKeyValuePair( new JString("mark"), new JSingleValue(rnd.Next(2, 5).ToString()) )
                );
            return testResult;
        }

    }
}