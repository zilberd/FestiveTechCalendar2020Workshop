using System.Threading;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace KedaFunctions
{
    public static class Subscriber
    {
        [FunctionName("Subscriber")]
        public static async System.Threading.Tasks.Task RunAsync([RabbitMQTrigger("k8queue", ConnectionStringSetting = "RabbitMQConnection")] string myQueueItem,
        ILogger log,
        CancellationToken cts,
        [Queue("k8queueresults", Connection = "AzureWebJobsStorage")] IAsyncCollector<string> messages)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            await messages.AddAsync($"Processed: {myQueueItem}", cts);
        }
    }
}