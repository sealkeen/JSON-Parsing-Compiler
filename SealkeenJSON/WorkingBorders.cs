using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SealkeenJSON
{
    sealed class WorkingBorders
    {
        public int left;
        public int right;
        public WorkingBorders innerBorders;

        public WorkingBorders(int start, int end)
        {
            left = start;
            right = end;
        }

        public int FullLength
        {
            get
            {
                if (right <= left)
                    return 0;
                return right - left;
            }
        }

        public int Length
        {
            get
            {
                if (right <= left)
                    return 0;
                return right - left;
            }
        }

        public void Set(int start, int end)
        {
            left = start;
            right = end;
        }

        public bool IsBetween(int value)
        {
            return (left < value && value < right);
        }
    }
}
