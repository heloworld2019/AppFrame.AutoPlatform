using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.AppAttribute.Assembly
{
    public class AssemblyAuthorAttribute : Attribute
    {
        public AssemblyAuthorAttribute(string author)
        {
            Author = author;
        }
        public string Author { get; private set; }
    }
}
