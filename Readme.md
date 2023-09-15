## Overview
Linq-to-XML styled project to create static-typed JSON Objects, e.g. - JString and JSingleValue for ("string" and 45).

## Test case. Reading from appSettings.Development.json.
```C#
        [Test]
        public void ParseTest()
        {
            JSONParser jSONParser = new JSONParser("O:\\VCSharp\\MediaStreamer.Web\\AuthMVCApplication\\appsettings.Development.json");
            var jItem = jSONParser.Parse();
            var logLevel = jItem.FindPairByKey(new JString("LogLevel"));
            var defaultLogLevel = logLevel.FindPairByKey(new JString("Default"));
            Debug.WriteLine(jItem); 
            // {"Logging":{"LogLevel":{"Default":"Information","Microsoft":"Warning","Microsoft.Hosting.Lifetime":"Information"}}}
            Debug.WriteLine(logLevel); 
            // "LogLevel":{"Default":"Information","Microsoft":"Warning","Microsoft.Hosting.Lifetime":"Information"}
            Debug.WriteLine(logLevel.Value); 
            // {"Default":"Information","Microsoft":"Warning","Microsoft.Hosting.Lifetime":"Information"}
            Debug.WriteLine(defaultLogLevel); 
            // "Default":"Information"
            Debug.WriteLine(defaultLogLevel.Value); 
            // "Information"
        }
```
## Test case. Writing.
```C#
    [Fact] // for XUnit [Test] //For NUnit
    public void CreateAndOpenJObjectFile()
    {
        // No parent element so first parameter is null
        JObject jObject = new JObject(null, 
            new JKeyValuePair(
                new JString("Key"), new JString("Value")
            )
        );

        jObject.SaveToFileAndOpenInNotepad("jKeyValuePair.txt");
    }

    // Result in "jKeyValuePair.txt"
    // {"Key":"Value"}
```

## Usage - output:
You can output the created objects using public methods like JItem.ToString() and JItem.ToFile("filename.txt"). 
See unit-tests for more info.

## Usage - JSONParser class
Create a JSONParser class by calling Constructor (string filename)

Create a JSONParser class by calling Constructor (string[] JSONString) 
** first element[0] of JSONString represents the in-memory JSONString that the parser will parse through.
The next step is the in-memory objects represented :

**public abstract class JItem** 

**public abstract class JCollection**

*JKeyValuePair class = "key" : "value"                             // (JKeyValuePair): JCollection*

*JSingleValue = false || true || 1234 || 194.0                     // (JSingleValue) : JItem (bool, integer, double, string)*

*JString = "JString object" || "any string value"                  // (JString) : (JSingleValue)*

*JObject =                                                         // (JObject) : JCollection*
    *{ "JString" : true } || 
    *{ { "JObject" }, { true }, { "JString" } } || 
    *{ "Key" : "Value" } // JObject : JItem
    
*JArray = [ { "JString" }, { "JObject" }, [ "JArray" ] ]           // (JArray) : JCollection*
