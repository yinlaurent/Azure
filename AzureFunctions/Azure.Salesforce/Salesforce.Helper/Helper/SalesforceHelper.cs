using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Salesforce.Helper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Salesforce.Helper.Helper
{
    public class SalesforceHelper : ISalesforceHelper
    {
        private SalesforceSettings _salesforceSettings;

        public SalesforceHelper(string sfdcConsumerKey, string sfdcConsumerSecret, string sfdcUserName, string sfdcPassword, string sfdcToken)
        {
            _salesforceSettings = new SalesforceSettings()
            {
                //set OAuth key and secret variables
                SfdcConsumerKey = sfdcConsumerKey,
                SfdcConsumerSecret = sfdcConsumerSecret,
                //set to Force.com user account that has API access enabled
                SfdcUserName = sfdcUserName,
                SfdcPassword = sfdcPassword,
                SfdcToken = sfdcToken
            };
        }

        public async Task<SalesforceAccessToken> GetAccessTokenAsync()
        {
            string oauthToken, serviceUrl;

            //create login password value
            string loginPassword = _salesforceSettings.SfdcPassword + _salesforceSettings.SfdcToken;
            using (HttpClient authClient = new HttpClient())
            {

                HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"grant_type","password"},
                        {"client_id", _salesforceSettings.SfdcConsumerKey},
                        {"client_secret", _salesforceSettings.SfdcConsumerSecret},
                        {"username", _salesforceSettings.SfdcUserName},
                        {"password",loginPassword}
                    }
                );

                HttpResponseMessage message = await authClient.PostAsync("https://login.salesforce.com/services/oauth2/token", content);

                string responseString = await message.Content.ReadAsStringAsync();

                dynamic obj = JsonConvert.DeserializeObject(responseString); ;
                
                oauthToken = (string)obj.access_token;
                serviceUrl = (string)obj.instance_url;
            }

            return new SalesforceAccessToken
            {
                oauthToken = oauthToken,
                serviceUrl = serviceUrl
            };
        }

        public async Task<string> GetAccountAsync(string accountName)
        {
            SalesforceAccessToken token = await GetAccessTokenAsync();
            string result = "[]";

            if (token != null)
            {
                using (HttpClient queryClient = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    //QUERY: Retrieve records of type "account"
                    //string restQuery = serviceUrl + "/services/data/v25.0/sobjects/Account";
                    //QUERY: retrieve a specific account
                    //string restQuery = serviceUrl + "/services/data/v25.0/sobjects/Account/001E000000N1H1O";
                    //QUERY: Perform a SELECT operation
                    string restQuery = token.serviceUrl + "/services/data/v25.0/query?q=SELECT+name+from+Account";

                    if (!String.IsNullOrWhiteSpace(accountName))
                    {
                        restQuery += "+where+name+=+'" + accountName + "'";
                    }

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, restQuery);

                    //add token to header
                    request.Headers.Add("Authorization", "Bearer " + token.oauthToken);

                    //return JSON to the caller
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //call endpoint async
                    HttpResponseMessage response = await queryClient.SendAsync(request);

                    result = await response.Content.ReadAsStringAsync();

                    dynamic obj = JsonConvert.DeserializeObject(result); ;

                    if (!String.IsNullOrWhiteSpace(accountName))
                    {
                        if ((int)(obj["totalSize"]) == 1)
                        {
                            restQuery = token.serviceUrl + (string)(obj["records"][0]["attributes"]["url"]);
                            request = new HttpRequestMessage(HttpMethod.Get, restQuery);
                            request.Headers.Add("Authorization", "Bearer " + token.oauthToken);
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            response = await queryClient.SendAsync(request);
                            result = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }

            return result;
        }

        public async Task<string> PostAccountAsync(dynamic accountData)
        {
            SalesforceAccessToken token = await GetAccessTokenAsync();
            string result = "[]";

            if (token != null)
            {
                using (HttpClient queryClient = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string restQuery = token.serviceUrl + "/services/data/v39.0/sobjects/Account/";

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, restQuery);
                    //add token to header
                    request.Headers.Add("Authorization", "Bearer " + token.oauthToken);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent httpContent = new StringContent(accountData.ToString(), Encoding.UTF8);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Content = httpContent;

                    //call endpoint async
                    HttpResponseMessage response = await queryClient.SendAsync(request);

                    result = await response.Content.ReadAsStringAsync();
                }
            }

            return result;
        }
    }
}
