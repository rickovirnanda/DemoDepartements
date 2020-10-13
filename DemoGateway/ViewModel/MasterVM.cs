using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGateway.ViewModel
{   public class SuccessResponse
    {
        public bool Success { get; set; }
        public string Reason { get; set; }

        public SuccessResponse()
        {
            Reason = string.Empty;
        }
    }
}
