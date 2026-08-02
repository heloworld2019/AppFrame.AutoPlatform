using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.AppAttribute.Assembly
{
    public class AssemblyUpdateTimeAttribute
    {
        public AssemblyUpdateTimeAttribute(string dateTime)
        {
            UpdateTime = dateTime;
        }
        public string UpdateTime { get; private set; }
    }
}
