using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Interfaces
{
    public interface IAppIpAddress
    {
        string IpAddress { get; set; }
        int Port { get; set; }
    }
}
