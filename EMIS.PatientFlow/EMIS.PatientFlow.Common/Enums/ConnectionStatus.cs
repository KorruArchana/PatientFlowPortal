using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMIS.PatientFlow.Common.Enums
{
    public enum ConnectionStatus
    {
        Offline = 0,
        Online = 1,
        Restart = 2,
        NotReachable = 3,
        NotConfigured = 4
    }
}
