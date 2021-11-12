## Overview
Linq-to-XML styled project to create static-typed JSON Objects, e.g. - JString and JSingleValue for ("string" and 45).

## Usage - output:
You can output the created objects using public methods like JItem.ToString() and JItem.ToFile("filename.txt"). 
See unit-tests for more info.

## Usage - JSONParser class
Create a JSONParser class by calling Constructor (string filename)

Create a JSONParser class by calling Constructor (string[] JSONString) 
// first element[0] of JSONString represents the in-memory JSONString that the parser will parse through.
The next step is the in-memory objects represented :

// public abstract class JItem 

// public abstract class JCollection
//JKeyValuePair class = "key" : "value"                             // (JKeyValuePair): JCollection

//JSingleValue = false || true || 1234 || 194.0                     // (JSingleValue) : JItem (bool, integer, double, string)

//JString = "JString object" || "any string value"                  // (JString) : (JSingleValue)

//JObject =                                                         // (JObject) : JCollection 
    { "JString" : true } || 
    { { "JObject" }, { true }, { "JString" } } || 
    { "Key" : "Value" } // JObject : JItem
    
//JArray = [ { "JString" }, { "JObject" }, [ "JArray" ] ]           // (JArray) : JCollection
