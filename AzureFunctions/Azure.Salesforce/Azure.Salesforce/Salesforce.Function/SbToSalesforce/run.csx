#r "System.Runtime.Serialization"
#r "Microsoft.ServiceBus"
#r "Salesforce.Helper.dll"
#r "Newtonsoft.Json"

using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Microsoft.ServiceBus.Messaging;
using Salesforce.Helper.Helper;
using Newtonsoft.Json.Linq;



public static void Run(AccountMessage sbMsg, TraceWriter log)
{
    log.Info($"Start C# ServiceBus topic trigger function processed message: {sbMsg.Name}");

    string sfdcConsumerKey = ConfigurationManager.AppSettings["Salesforce.SfdcConsumerKey"];
    string sfdcConsumerSecret = ConfigurationManager.AppSettings["Salesforce.SfdcConsumerSecret"];
    string sfdcUserName = ConfigurationManager.AppSettings["Salesforce.SfdcUserName"];
    string sfdcPassword = ConfigurationManager.AppSettings["Salesforce.SfdcPassword"];
    string sfdcToken = ConfigurationManager.AppSettings["Salesforce.SfdcToken"];

    var helper = new SalesforceHelper(sfdcConsumerKey, sfdcConsumerSecret, sfdcUserName, sfdcPassword, sfdcToken);
    dynamic data = TransformAccountMessage(sbMsg);
    string result = helper.PostAccountAsync(data).Result;

    log.Info($"End C# ServiceBus topic trigger function processed message: {sbMsg.Name}");
}

public class AccountMessage
{
    public string Name { get; set; }
    public string BillingStreet { get; set; }
    public string BillingPostalCode { get; set; }
    public string BillingCity { get; set; }
    public string BillingCountry { get; set; }
}

public static dynamic TransformAccountMessage(AccountMessage accountMessage)
{
    dynamic data = new JObject();
    data.Name = accountMessage.Name;
    data.BillingStreet = accountMessage.BillingStreet;
    data.BillingPostalCode = accountMessage.BillingPostalCode;
    data.BillingCity = accountMessage.BillingCity;
    data.BillingCountry = accountMessage.BillingCountry;
    return data;
}