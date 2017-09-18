using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Azure.QueueTrigger.QueueTrigger
{
    public class QueueTriggerFunction
    {
        public static async Task Run(string path, TraceWriter log)
        {
            string storageConnectionString = ConfigurationManager.AppSettings["StorageAccount.ConnectionString"];

            log.Info($"Executing {typeof(QueueTriggerFunction).Name} on file {path}");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("data");
            await container.CreateIfNotExistsAsync();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(path);

            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
                memoryStream.Position = 0;
                //Proceed with stream
            }
        }
    }
}