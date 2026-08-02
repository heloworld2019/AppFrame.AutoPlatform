using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Entity.Dto
{
    public class AppInputDto
    {
        public string InputName { get; set; }
        public int Index { get; set; }
        public string ParameterName { get; set; }
        public string ParameterDescription { get; set; }
        public bool Enable { get; set; }
        public bool Value { get; set; }
    }
}
