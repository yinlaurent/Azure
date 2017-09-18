using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Azure.QueueTrigger.QueueTrigger
{
    public class QueueTriggerFunction
    {
        public static async Task Run(string path, TraceWriter log)
        {
            string hotAzureStorageConnectionString = ConfigurationManager.AppSettings["StorageAccount.ConnectionString"];

            log.Info($"Executing {typeof(QueueTriggerFunction).Name} on file {path}");

            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(hotAzureStorageConnectionString);
        }
    }
}