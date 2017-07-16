using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gab2017
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/", Name = "Submission_x0023_0.AccountMessage")]
    public class AccountMessage
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string BillingStreet { get; set; }
        [DataMember]
        public string BillingPostalCode { get; set; }
        [DataMember]
        public string BillingCity { get; set; }
        [DataMember]
        public string BillingCountry { get; set; }
    }
}
