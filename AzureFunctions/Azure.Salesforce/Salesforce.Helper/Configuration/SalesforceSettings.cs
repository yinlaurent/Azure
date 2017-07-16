using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salesforce.Helper.Configuration
{
    public class SalesforceSettings
    {
        public string SfdcConsumerKey { get; set; }
        public string SfdcConsumerSecret { get; set; }
        public string SfdcUserName { get; set; }
        public string SfdcPassword { get; set; }
        public string SfdcToken { get; set; }
    }
}
