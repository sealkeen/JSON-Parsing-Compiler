using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


enum BracketSelector { Curly, Square }

namespace SealkeenJSON
{
    public partial class testForm : Form
    {
        static bool endParsing = false;

        int nestingLevel = 0;
        //private int lastEnded

        int currentNodeElements = 0;
        //count of elements within the current node

        Dictionary<int, int> nodesPerLevel = new Dictionary<int, int>();

        //A queue where to keep our objects
        Queue q = new Queue(); 

        private int[] workingInterval = new int[2];
        //The working interval 
        //example: [500, 750] - these chars we are going to parse


        //To get the total lines count, we should ++ it
        private int indexOfTheChar = -1;

        //Write the index of the string where we first found the char
        Stack<int> openingCurlyBrackets = new Stack<int>();
        Stack<int> closingCurlyBrackets = new Stack<int>();

        Stack<int> openingSquareBrackets = new Stack<int>();
        Stack<int> closingSquareBrackets = new Stack<int>();

        Stack<int> openingQuotes = new Stack<int>();
        Stack<int> closingQuotes = new Stack<int>();

        Stack<int> colons = new Stack<int>();
        Stack<int> commas = new Stack<int>();
        Stack<int> newLines = new Stack<int>();

        Dictionary<string, string> KVpAir = new Dictionary<string, string>();

        Dictionary<int, int> parsedAreas = new Dictionary<int, int>();

        public testForm()
        {
            InitializeComponent();
        }

        private void ParserForm_Load(object sender, EventArgs e)
        {

        }

        private Dictionary<string, string> ParseWithinBrackets(BracketSelector brackets)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            
            if (IsParsedAlready(GetStartingIntervalPoint()))
            { values.Add("", ""); return values; }
            if (GetWorkingLength() < 4)
            {  values.Add("", ""); return values; }
            bool isThereColons = colons.Count != 0 ? IsBetweenWorkingInterval(colons.Peek()) : false;
            bool isThereCommas = commas.Count != 0 ? (IsBetweenWorkingInterval(commas.Peek())) : false;
                if (!isThereColons)
                { values.Add("", ""); return values; }


            var query = textBoxMain.Text.Substring
                (GetStartingIntervalPoint(),
                GetWorkingLength()).Split(',');

            foreach (string str in query)
            {
                if (!str.Contains(":"))
                    continue;
                var splitedPair = str.Split(':');
                try
                {
                    values.Add(splitedPair[0].Replace(" ", ""), splitedPair[1].Replace(" ", ""));
                }
                catch { }
            }

            currentNodeElements = values.Count;
            return values;
            //textBoxMain.Text.Substring(,);
            //jsonDocument.Add();
        }

        private const double openingSignDistinction = 0.0;
        private const double closingSignDistinction = 0.3;
        private bool isOpeningQuotes = false;

        public bool isStructuralToken(char ch)
        {
            ++indexOfTheChar;
            switch (ch)
            {
                case '{':
                    openingCurlyBrackets.Push(indexOfTheChar);
                    //AnalyzeCodeBetweenLastBrackets();
                    break;
                case '}':
                    closingCurlyBrackets.Push(indexOfTheChar);
                    SetWorkingInterval(openingCurlyBrackets.Peek(), closingCurlyBrackets.Peek());
                    //AnalyzeCodeBetweenLastBrackets();
                    q.Enqueue(ParseWithinBrackets(BracketSelector.Curly));
                    //Debug.WriteLine(q.Dequeue());
                    AddParsedArea(GetStartingIntervalPoint(), GetClosingIntervalPoint());
                    openingCurlyBrackets.Pop();
                    closingCurlyBrackets.Pop();
                    nodesPerLevel.Add(nestingLevel, currentNodeElements);
                    ++nestingLevel;
                    break;                                              
                case '[':
                    openingSquareBrackets.Push(indexOfTheChar);
                    //AnalyzeCodeBetweenLastBrackets();
                    break;
                case ']':
                    closingSquareBrackets.Push(indexOfTheChar);
                    SetWorkingInterval(openingSquareBrackets.Peek(), closingSquareBrackets.Peek());
                    //AnalyzeCodeBetweenLastBrackets();
                    q.Enqueue(ParseWithinBrackets(BracketSelector.Square));
                    //Debug.WriteLine(q.Dequeue());
                    AddParsedArea(GetStartingIntervalPoint(), GetClosingIntervalPoint());
                    openingSquareBrackets.Pop();
                    closingSquareBrackets.Pop();
                    ++nestingLevel;
                    break;
                case '\"':
                    isOpeningQuotes = !isOpeningQuotes;
                    if (isOpeningQuotes)
                        openingQuotes.Push(indexOfTheChar);
                    else
                        closingQuotes.Push(indexOfTheChar);
                    break;
                case ':':
                    colons.Push(indexOfTheChar);
                    break;
                case ',':
                    commas.Push(indexOfTheChar);            
                    break;
                case '\n':
                    newLines.Push(indexOfTheChar);
                    break;
                default:
                    return false;
            }
            return true;
        }
        
        
        private int currentNodeCount = 0;
        private int currentLevelCount = 0;
        private TreeNode GetFullTree(TreeNode child)
        {
            TreeNode parent = new TreeNode();
            try
            {
                var node = q.Dequeue() as Dictionary<string, string>;
                foreach(var cldNd in node)
                parent.Nodes.Add(cldNd.ToString());
                parent.FirstNode.Name = node.First().Key.ToString();
                parent.Nodes.Add(child);

            } catch { }
            currentNodeCount++;
            if (currentNodeCount <= nodesPerLevel.Count)
                return GetFullTree(parent);
            else
                return parent;
        }

        //void RenewWorkingInterval(Brackets brackets)
        //{
        //    switch(brackets)
        //    {
        //        case Brackets.Curly:
        //        workingInterval[0] = openingCurlyBrackets.Peek();
        //        workingInterval[1] = closingCurlyBrackets.Peek();
        //            break;
        //        case Brackets.Square:
        //        workingInterval[0] = openingSquareBrackets.Peek();
        //        workingInterval[1] = closingSquareBrackets.Peek();
        //            break;
        //    }
            
        //}

        private int GetStartingIntervalPoint()
        {
            return workingInterval[0];
        }

        private int GetClosingIntervalPoint()
        {
            return workingInterval[1];
        }

        private void SetWorkingInterval(int start, int end)
        {
            workingInterval[0] = start;
            workingInterval[1] = end;
        }

        private int GetWorkingLength()
        {
            return workingInterval[1] - (workingInterval[0]);
        }

        private bool IsBetweenWorkingInterval(int value)
        {
            return (workingInterval[0] < value && value < workingInterval[1]);
        }

        private void AnalyzeCodeBetweenLastBrackets()
        {

        }

        private bool IsParsedAlready(int index)
        {
            foreach (var KVp in parsedAreas)
            {
                if (KVp.Key <= index && index <= KVp.Value)
                    return true;
            }
            return false;
        }

        private void AddParsedArea(int start, int end)
        {
            parsedAreas.Add(start, end);
        }
   
        private bool IsOpeningBracket(double bracket)
        {
            return !( ((int)bracket) < bracket );
            //Compare number.3 (which is my interpretation of '}' or ']') 
            //and     number.0 (which is my interpretation of '{' or '{') 
        }

        //private bool TryParseOnlyValues(string s){}


        private void btnParse_Click(object sender, EventArgs e)
        {


            q.Clear();
            treeViewJSON.Nodes.Clear();
            ReinitializeEverything();
            string s = @"{{{{{{{}}}}}}}";

            //var structuralTokens = from ch in s where isStructuralToken(ch) select ch;

            //foreach (var ch in structuralTokens)
            //{
            //    textBoxMain.Text += ch;
            //}

            //textBoxMain.Text += $"linq loop count {indexOfTheChar}";
            //int len = 0;
            //for (int c = 0; c < textBoxMain.Text.Length; c++)
            //{
            //    len++;
            //}
            //textBoxMain.Text += $"{Environment.NewLine}for loop count {len}";

            for (int c = 0; c < textBoxMain.Text.Length; c++)
            {
                if (endParsing)
                    break;
                isStructuralToken(textBoxMain.Text[c]);
            }

            for(int c = 0; c< q.Count;++c)
            {
                //Debug.WriteLine(q.Dequeue());
            }

            treeViewJSON.Nodes.Add(GetFullTree(null));

            //var Kv = KVpAir.First();

            ////foreach (Dictionary<string, string> node in q)
            ////{
            ////    TreeNode Tn = new TreeNode();
            ////    Tn.Name = node.Keys.First();
            ////    foreach (KeyValuePair<string,String> KVp in node)
            ////        Tn.Nodes.Add(KVp.ToString());
            ////    treeViewJSON.Nodes.Add(Tn);
            ////}

            //treeViewJSON.Nodes[0].Nodes.Add("this\r\t is a string");
            //TreeNodeCollection tNC = treeViewJSON.Nodes;
            // treeViewJSON.Nodes.Insert(0, "node");
            //treeViewJSON.Nodes.

            //treeViewJSON.Nodes.Add("this\r\t is a string");
            //treeViewJSON.Nodes[0].Nodes.Add("this\r\t is a string");
            //TreeNodeCollection tNC = treeViewJSON.Nodes;
            //treeViewJSON.Nodes.Insert(0, "node");
            ////treeViewJSON.Nodes.
        }

        private void ParseOneLine(string line)
        {
            for(int i =0; i<line.Length; ++i)
            {
                switch(line[i])
                {

                }
            }

        }

        private void ReinitializeEverything()
        {
            SetWorkingInterval(0, 0);
            indexOfTheChar = - 1;
            openingCurlyBrackets.Clear();
            closingCurlyBrackets.Clear();

             openingSquareBrackets.Clear();
             closingSquareBrackets.Clear();

             openingQuotes.Clear();
             closingQuotes.Clear();

             colons.Clear();
             commas.Clear();
             newLines.Clear();
        }
        private void test_Click(object sender, EventArgs e)
        {
            textBoxMain.Text += Environment.NewLine;
            for(double i = 0.3; i < 100; i++)
                textBoxMain.Text += IsOpeningBracket(i);

            textBoxMain.Text += Environment.NewLine;
            for (double i = 0.0; i < 100; i++)
                textBoxMain.Text += IsOpeningBracket(i);
        }

        private void textBoxMain_TextChanged(object sender, EventArgs e)
        {

        }



        private Dictionary<string, string> Parse(string text)
        {
            Dictionary<string, string> ParsedValues = new Dictionary<string, string>();

            bool openedCurlyBr = false;
            bool openedSquareBr = false;
            for (int i = 0; i < textBoxMain.Text.Length; ++i)
            {
                string buffer = "";
                switch(textBoxMain.Text[i])
                {
                    case '{':
                        openingCurlyBrackets.Push(indexOfTheChar);
                        openedCurlyBr = true;
                        if (openedCurlyBr)
                            buffer += text[i];
                        break;
                    case '}':
                        closingCurlyBrackets.Push(indexOfTheChar);
                        SetWorkingInterval(openingCurlyBrackets.Peek(), closingCurlyBrackets.Peek());
                        //AnalyzeCodeBetweenLastBrackets();
                        q.Enqueue(ParseWithinBrackets(BracketSelector.Curly));
                        //Debug.WriteLine(q.Dequeue());
                        AddParsedArea(GetStartingIntervalPoint(), GetClosingIntervalPoint());
                        openingCurlyBrackets.Pop();
                        closingCurlyBrackets.Pop();
                        nodesPerLevel.Add(nestingLevel, currentNodeElements);
                        ++nestingLevel;

                        openedCurlyBr = false;
                        HandleString(buffer, ParsedValues);
                        buffer = "";
                        break;
                    case '[':
                        break;
                    case ']':
                        break;
                    case '\"':
                        isOpeningQuotes = !isOpeningQuotes;
                        if (isOpeningQuotes)
                            openingQuotes.Push(indexOfTheChar);
                        else
                            closingQuotes.Push(indexOfTheChar);
                        break;
                    case ':':
                        colons.Push(indexOfTheChar);
                        break;
                    case ',':
                        commas.Push(indexOfTheChar);
                        break;
                    case '\n':
                        newLines.Push(indexOfTheChar);
                        break;

                }
            }
            return ParsedValues;
        }

        private Dictionary<string, string> HandleString(string values, Dictionary<string, string> ParsedValues)
        {
            var query = values.Split(',');
            foreach (string str in query)
            {
                if (!str.Contains(":"))
                    continue;
                var splitedPair = str.Split(':');
                try
                {
                    ParsedValues.Add(splitedPair[0].Replace(" ", ""), splitedPair[1].Replace(" ", ""));
                }
                catch { }
            }
            return ParsedValues;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            frmTest t = new frmTest();
            t.Show();
        }
    }
}
