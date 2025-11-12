using Azure.Storage.Queues.Models;

namespace E2ETests.Interfaces
{
    public interface IMessageQueueService
    {
        /// <summary>
        /// Retrieves up to the specified number of messages from the given queue asynchronously.
        /// </summary>
        /// <param name="queueName">The name of the queue from which to read messages. Cannot be null or empty.</param>
        /// <param name="maximumMessages">The maximum number of messages to retrieve from the queue. Must be greater than zero. The default is 10.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an array of <see
        /// cref="QueueMessage"/> objects read from the queue. The array will be empty if no messages are available.</returns>
        Task<QueueMessage[]> ReadQueueAsync(string queueName, int maximumMessages = 10);

    }
}
