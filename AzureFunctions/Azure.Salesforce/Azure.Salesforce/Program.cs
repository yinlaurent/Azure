using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json.Linq;
using Salesforce.Helper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gab2017
{
    class Program
    {
        static void Main(string[] args)
        {
            string azureSbConnectionString = "<My Service Bus Connection String>";

            var accountCreation = new AccountMessage()
            {
                Name = "MyAccount",
                BillingStreet = "MyBillingStreet",
                BillingPostalCode = "75001",
                BillingCity = "Paris",
                BillingCountry = "France",
            };

            var message = new BrokeredMessage(accountCreation);
            TopicClient topicClient = TopicClient.CreateFromConnectionString(azureSbConnectionString);
            topicClient.Send(message);

        }
    }
}
