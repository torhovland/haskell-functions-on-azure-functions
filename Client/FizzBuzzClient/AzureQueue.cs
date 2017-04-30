using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace FizzBuzzClient
{
    internal class AzureQueue
    {
        private readonly CloudQueueClient queueClient;

        public AzureQueue(string connectionString)
        {
            queueClient = CloudStorageAccount
                .Parse(connectionString)
                .CreateCloudQueueClient();
        }

        public async Task WriteAsync(string queueName, string message)
        {
            var queue = queueClient.GetQueueReference(queueName);
            await queue.CreateIfNotExistsAsync();
            await queue.AddMessageAsync(new CloudQueueMessage(message));
        }

        public async Task<IEnumerable<string>> ReadAsync(string queueName)
        {
            var queue = queueClient.GetQueueReference(queueName);
            var cloudQueueMessages = await queue.GetMessagesAsync(10);
            return await Task.WhenAll(cloudQueueMessages.Select(m => ReadAndDeleteAsync(queue, m)));
        }

        async Task<string> ReadAndDeleteAsync(CloudQueue queue, CloudQueueMessage message)
        {
            await queue.DeleteMessageAsync(message);
            return message.AsString.Trim();
        }
    }
}