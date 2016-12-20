using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SealkeenJSON
{
    public class TokenChar
    {
        public int Position { get; set; } = -1;
        public char Symbol { get; set; }
    }
    

    class CBracket : TokenChar
    {
        public CBracket(char ch, ref int index)
        {
            Symbol = ch;
            Position = index;
        }
        public bool Opening
        {
            get
            {
                if (Symbol == '{')
                    return true;
                else
                    return false;
            }
        }
    }
    
    class SBracket : TokenChar
    {
        public SBracket(char ch, ref int index)
        {
            Symbol = ch;
            Position = index;
        }
        public bool Opening
        { get
            {
                if (Symbol == '[')
                    return true;
                else
                    return false;
            }
        }
    }



    class SingleToken : TokenChar
    {
        public SingleToken(char ch, ref int index)
        {
            Symbol = ch;
            Position = index;
        }
    }
}
