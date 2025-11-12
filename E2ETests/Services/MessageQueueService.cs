using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using E2ETests.Interfaces;
using Microsoft.Extensions.Configuration;

namespace E2ETests.Services
{
    public class MessageQueueService : IMessageQueueService
    {
        private readonly string _connectionString;
        
        /// <summary>
        /// Initializes a new instance of the MessageQueueService class using the specified configuration settings.
        /// </summary>
        /// <remarks>The constructor expects the configuration to contain a valid connection string under
        /// the key "casestorage-connectionstring". If the key is missing or the value is invalid, subsequent operations
        /// may fail.</remarks>
        /// <param name="configuration">The configuration provider used to retrieve application settings, including the message queue connection
        /// string. Cannot be null.</param>
        public MessageQueueService(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("casestorage-connectionstring")
                ?? throw new InvalidOperationException("Missing case storage connection string in config");
        }

        /// <summary>
        /// Retrieves up to the specified number of messages from the given queue asynchronously.
        /// </summary>
        /// <param name="queueName">The name of the queue from which to read messages. Cannot be null or empty.</param>
        /// <param name="maxMessages">The maximum number of messages to retrieve from the queue. Must be greater than zero. The default is 10.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an array of <see
        /// cref="QueueMessage"/> objects read from the queue. The array will be empty if no messages are available.</returns>
        public async Task<QueueMessage[]> ReadQueueAsync(string queueName, int maximumMessages = 10)
        {
            QueueClient queue = new QueueClient(_connectionString, queueName);

            return await queue.ReceiveMessagesAsync(maxMessages: maximumMessages);
        }

    }
}
