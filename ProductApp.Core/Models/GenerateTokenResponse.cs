using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Core.Models
{
    public class GenerateTokenResponse
    {
        public string Token { get; set; }
        public DateTime TokenExpireDate { get; set; }
    }
}
