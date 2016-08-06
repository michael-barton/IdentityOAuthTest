using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Paths
    {
        public const string AuthorizationServerBaseAddress = "http://localhost:50559/";
        public const string ResourceServerBaseAddress = "http://localhost:63249/";
        public const string TokenPath = @"/OAuth/Token";
        public const string ResourcePath = @"/api/Test";
        public const string AuthorizePath = "/OAuth/Authorize";
    }
}
