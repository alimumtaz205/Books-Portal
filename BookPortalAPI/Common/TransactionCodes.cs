using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Models
{
    public enum TranCodes
    {
        Unknown = -1,
        Success = 100,
        Exception = 2035,
        Invalid_Cipher = 1002,
        Invalid_Client = 1003,
        Invalid_Grant = 1004,
        Invalid_Header_Length = 1005,
        VERSION_NOT_MATCH = 1006,
        UNAUTHORIZED_REQUEST = 1007,
        X_COMPUTED_NOT_MATCHED = 1008
    }
}
