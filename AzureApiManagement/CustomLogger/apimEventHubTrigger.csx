#r "../bin/Microsoft.Azure.EventHubs.dll"
#r "Newtonsoft.Json"
#r "Microsoft.ApplicationInsights"

using System;
using System.Text;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json.Linq;
using Microsoft.ApplicationInsights;

public static async Task Run(EventData[] events, ILogger log)
{
    var exceptions = new List<Exception>();

    var telemetryClient = new TelemetryClient();

    foreach (EventData eventData in events)
    {
        try
        {
            string messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);

            var eventDetail = new Dictionary<string, string>();
            JObject json = JObject.Parse(messageBody);
            foreach (JProperty property in json.Properties())
            {
                eventDetail.Add(property.Name, property.Value.ToString());
            }

            foreach(var item in eventDetail)
            {
                log.LogInformation(item.Key + " - " + item.Value);
            }

            telemetryClient.TrackEvent("APIM Logs", eventDetail);
            await Task.Yield();
        }
        catch (Exception e)
        {
            // We need to keep processing the rest of the batch - capture this exception and continue.
            // Also, consider capturing details of the message that failed processing so it can be processed again later.
            exceptions.Add(e);
        }
    }
    // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

    if (exceptions.Count > 1)
        throw new AggregateException(exceptions);

    if (exceptions.Count == 1)
        throw exceptions.Single();

}
