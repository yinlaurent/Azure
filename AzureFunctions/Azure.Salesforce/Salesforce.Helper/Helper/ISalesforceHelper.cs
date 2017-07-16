using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salesforce.Helper.Helper
{
    public interface ISalesforceHelper
    {
        Task<SalesforceAccessToken> GetAccessTokenAsync();
    }
}
