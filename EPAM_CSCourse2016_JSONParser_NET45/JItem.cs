using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EPAM.CSCourse2016.SilkinIvan.SealkeenJSON
{
    //Represents the structure of a JSON document
    public abstract class JItem
    {
        protected List<JItem> Items;
        public JItem Parent = null;
        public JItem(JItem parent = null)
        {
            Parent = parent;
        }
        public bool ToFile(string filename, bool rewrite = false)
        {
            StreamWriter sW;
            if (File.Exists(filename) && rewrite)
            {
                sW =  new StreamWriter(filename, !rewrite);
            }
            sW = new StreamWriter(filename, rewrite);
            sW.Write(ToString());
            sW.Close();
            return true;
        }
        public void ListAllNodes(ref List<JItem> nodes)
        {
            if (Items != null)
            {
                foreach (var jItem in Items)
                {
                    nodes.Add(jItem);
                }
            }
        }
        public virtual bool HasItems()
        {
            return false;
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            BuildString(ref builder);
            return builder.ToString();
        }
        public virtual void BuildString(ref StringBuilder builder)
        {
            BuildString(ref builder);
        }
    }
}
