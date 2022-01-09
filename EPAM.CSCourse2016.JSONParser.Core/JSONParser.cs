//CREATED BY SILKIN IVAN AT EPAM TRAINING 2016

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
//using EPAM.CSCourse2016.ParserPerfTester.Common;
using System.IO;

namespace EPAM.CSCourse2016.JSONParser.Library
{
    public class JSONParser //: IParser
    {
        public readonly char[] EmptySymbols = { ' ', '\t', '\r', '\n' };

        private Stack<char> _syntaxChars = new Stack<char>(2);
        private Stack<JItem> _itemStack = new Stack<JItem>(2);
        private Stack<JKeyValuePair> _keyValueStack = new Stack<JKeyValuePair>(2);
        private JItem _currentItem;
        private JKeyValuePair _currentKeyValuePair;

        private StringBuilder _JItemContentsBuffer = new StringBuilder("");
        private StringBuilder _sourceString;

        private bool _isCharacterInsideQuotes = false;
        private bool _pendingForPairValue = false;
        private int _indexOfTheChar = -1;
        public JSONParser()
        {
            InitializeStructure();
        }
        /// <summary>
        /// Parses JSON file.
        /// </summary>
        /// <param name="filePath">Parse JSON from a file.</param>
        public JSONParser(string filePath) : this()
        {
            if (File.Exists(filePath))
            {
                StreamReader streamReader = new StreamReader(filePath);
                _sourceString = new StringBuilder(streamReader.ReadToEnd());
            }
        }

        /// <summary>
        /// Parses in-memory JSON string.
        /// </summary>
        /// <param name="JSON">The first element [0] is the string to parse.</param>
        public JSONParser(string[] JSON) : this()
        {
            if (JSON.Length > 0)
            {
                _sourceString = new StringBuilder(JSON[0]);
            }
        }

        private JItem InitializeStructure()
        {
            JKeyValuePair newPair = new JKeyValuePair(null, null);
            JRoot Root = new JRoot();
            _keyValueStack.Clear();
            _itemStack.Clear();
            _syntaxChars.Clear();

            _keyValueStack.Push(newPair);
            _itemStack.Push(Root);
            _syntaxChars.Push(',');

            _indexOfTheChar = -1;
            _currentItem = Root;
            return _itemStack.First();
        }

        public JItem Parse()
        {
            InnerParse();
            FindRootJSItem();
            return _currentItem;
        }

        private void InnerParse()
        {
            try
            {
                for (int c = _indexOfTheChar; c < _sourceString.Length; c++)
                {
                    ParseNextSymbol();
                }
            }
            catch
            {
                if (_sourceString.Length > (_indexOfTheChar + 1)) //if inccorrect symbol is inside of the string
                {
                    Debug.WriteLine($"Unexpected symbol under index: {_indexOfTheChar}. Total Length : {_sourceString.Length}");
                    InnerParse();
                }
            }
            if (_itemStack.Count == 1 && _keyValueStack.Count == 1)
            {
                _currentItem = _itemStack.Peek();
                if (_currentItem is JRoot && !_currentItem.HasItems())
                {
                    (_currentItem as JRoot).Add(new JSingleValue(_JItemContentsBuffer.Replace(" ", "").ToString(), _currentItem));
                    _JItemContentsBuffer.Clear();
                }
            }
        }

        public string ToTestString(string str)
        {
            _sourceString = new StringBuilder(str);
            InitializeStructure();
            InnerParse();
            FindRootJSItem();
            return _currentItem.ToString();
        }

        public JItem GetCurrentItem()
        {
            return _currentItem;
        }

        private void FindRootJSItem()
        {
            while (_currentItem.Parent != null)
            {
                _currentItem = _currentItem.Parent;
            }
        }

        /// <summary>
        /// The Main Parsing Method
        /// </summary>
        private void ParseNextSymbol()
        {
            ++_indexOfTheChar;

            if (!ParseSymbolInsideQuotes())
            {
                InspectSymbol(_sourceString[_indexOfTheChar]);
            }
        }

        private void InspectSymbol(char symbol)
        {
            if (EmptySymbols.Contains(symbol))
                return;
            switch (symbol)
            {
                case '{': HandleCurlyBracket(); break;
                case '}': HandleClosingCurlyBracket();  break;
                case '[': HandleSquareBracket(); break;
                case ']': HandleClosingSquareBracket(); break;
                case ':': HandleColon(); break;
                case ',': HandleComma(); break;
                case '\"':
                    _JItemContentsBuffer.Append(_sourceString[_indexOfTheChar]);
                    _syntaxChars.Push('\"');
                    _isCharacterInsideQuotes = !_isCharacterInsideQuotes;
                    break;
                default:
                    _JItemContentsBuffer.Append(_sourceString[_indexOfTheChar]);
                    break;
            }
        }

        private void HandleComma()
        {
            _currentKeyValuePair = _keyValueStack.Peek();
            _currentItem = _itemStack.Peek();
            if (_pendingForPairValue)
            {
                JItem jI = new JSingleValue(_JItemContentsBuffer.ToString(), _currentItem);
                _currentKeyValuePair.Value = jI;
                _keyValueStack.Pop();
                _pendingForPairValue = false;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(_JItemContentsBuffer.ToString()))
                {
                    (_currentItem as JCollection).Add(new JSingleValue(_JItemContentsBuffer.ToString(), _currentItem));
                    _JItemContentsBuffer.Clear();
                    return;
                }
            }
            _syntaxChars.Push(',');
            _JItemContentsBuffer.Clear();
        }

        private void HandleColon()
        {
            _currentKeyValuePair = _keyValueStack.Peek();
            _currentItem = _itemStack.Peek();
            _syntaxChars.Push(':');
            if (!_pendingForPairValue)
            {
                JKeyValuePair pair =
                    new JKeyValuePair(new JSingleValue(_JItemContentsBuffer.ToString()), null);
                (_currentItem as JCollection).Add(pair);
                _keyValueStack.Push(pair);
            }
            else
            {
                //throw new InvalidEnumArgumentException();
            }
            _pendingForPairValue = true;
            _JItemContentsBuffer.Clear();
        }

        private void HandleClosingSquareBracket()
        {
            _currentKeyValuePair = _keyValueStack.Peek();
            _currentItem = _itemStack.Peek();
            if (_pendingForPairValue)
            {
                JItem item = new JSingleValue(_JItemContentsBuffer.ToString(), _currentItem);
                _currentKeyValuePair.Value = item;
                _JItemContentsBuffer.Clear();
                _keyValueStack.Pop();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(_JItemContentsBuffer.ToString()))
                {
                    (_currentItem as JCollection).Add(new JSingleValue(_JItemContentsBuffer.ToString(), _currentItem));
                    _JItemContentsBuffer.Clear();
                }
            }
            //currentItem = currentItem.Parent;
            _itemStack.Pop();
            _pendingForPairValue = false;
        }

        private void HandleSquareBracket()
        {
            _currentKeyValuePair = _keyValueStack.Peek();
            _currentItem = _itemStack.Peek();
            if (_pendingForPairValue)
            {
                JItem item = new JArray(_currentItem);
                _currentKeyValuePair.Value = item;
                _JItemContentsBuffer.Clear();
                CacheKeyValueAndCurrentItem(item);
                _pendingForPairValue = false;
            }
            else
            {
                JArray nItm = new JArray(_currentItem);
                (_currentItem as JCollection).Add(nItm);
                nItm.Parent = _currentItem;
                _itemStack.Push(nItm);
            }
        }

        private void HandleClosingCurlyBracket()
        {
            _currentKeyValuePair = _keyValueStack.Peek();
            _currentItem = _itemStack.Peek();
            if (_pendingForPairValue)
            {
                JItem item = new JSingleValue(_JItemContentsBuffer.ToString(), _currentItem);
                _currentKeyValuePair.Value = item;
                _JItemContentsBuffer.Clear();
                _keyValueStack.Pop();
                _pendingForPairValue = false;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(_JItemContentsBuffer.ToString()))
                {
                    (_currentItem as JCollection).Add(new JSingleValue(_JItemContentsBuffer.ToString(), _currentItem));
                    _JItemContentsBuffer.Clear();
                }
            }
            _itemStack.Pop();
        }

        private void HandleCurlyBracket()
        {
            _currentKeyValuePair = _keyValueStack.Peek();
            _currentItem = _itemStack.Peek();
            if (_pendingForPairValue)
            {
                JObject item = new JObject(_currentItem);
                _currentKeyValuePair.Value = item;
                _JItemContentsBuffer.Clear();
                CacheKeyValueAndCurrentItem(item);
                _pendingForPairValue = false;
            }
            else
            {
                JObject nItm = new JObject(_currentItem);
                (_currentItem as JCollection).Add(nItm);
                nItm.Parent = _currentItem;
                _itemStack.Push(nItm);
            }
        }

        private void CacheKeyValueAndCurrentItem(JItem item)
        {
            _keyValueStack.Push(_currentKeyValuePair);
            _itemStack.Push(item);
        }

        private bool ParseSymbolInsideQuotes()
        {
            if (_isCharacterInsideQuotes)
            {
                if (_syntaxChars.Peek() == '\"')
                {
                    if (_sourceString[_indexOfTheChar] == '\"')
                    {
                        _JItemContentsBuffer.Append(_sourceString[_indexOfTheChar]);
                        _isCharacterInsideQuotes = false;
                        return true;
                    }
                    _JItemContentsBuffer.Append(_sourceString[_indexOfTheChar]);
                    return true;
                }
            }
            return false;
        }
    }
}
