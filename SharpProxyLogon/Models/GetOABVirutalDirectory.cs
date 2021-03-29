using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpProxyLogon.Models
{


    public class GetOABVirutalDirectory
    {
        public D d { get; set; }
    }

    public class D
    {
        public string __type { get; set; }
        public string[] Cmdlets { get; set; }
        public object[] ErrorRecords { get; set; }
        public object[] Informations { get; set; }
        public bool IsDDIEnabled { get; set; }
        public object[] Warnings { get; set; }
        public object[] NoAccessProperties { get; set; }
        public Output[] Output { get; set; }
        public string[] ReadOnlyProperties { get; set; }
        
    }


    public class Server
    {
        public string __type { get; set; }
        public string Type { get; set; }
    }

    public class Internalurl
    {
        public string __type { get; set; }
        public string Type { get; set; }
        public string ExpectedUriKind { get; set; }
    }

    public class Externalurl
    {
        public string __type { get; set; }
        public string Type { get; set; }
        public string ExpectedUriKind { get; set; }
    }

    public class Pollinterval
    {
        public string __type { get; set; }
        public string Type { get; set; }
        public bool AcceptNull { get; set; }
        public bool AcceptUnlimted { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
    }

    public class Name
    {
        public string __type { get; set; }
        public string Type { get; set; }
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public string DisplayCharacters { get; set; }
        public string InvalidCharacters { get; set; }
    }

    public class Output
    {
        public Identity Identity { get; set; }

    }

    public class Identity
    {
        public string __type { get; set; }
        public string DisplayName { get; set; }
        public string RawIdentity { get; set; }
    }

  
}
