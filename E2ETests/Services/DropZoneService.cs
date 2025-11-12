using E2ETests.Interfaces;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;


namespace E2ETests.Services
{
    /// <summary>
    /// Provides functionality for managing files within a drop zone, including moving .svs files from the archive
    /// directory to the main directory in an Azure File Share.
    /// </summary>
    /// <remarks>This service is intended for use with Azure File Shares and requires a valid connection
    /// string and share name to operate. It encapsulates file management operations specific to the drop zone scenario,
    /// such as relocating files between directories. Thread safety is not guaranteed; callers should ensure appropriate
    /// synchronization if accessed concurrently.</remarks>
    public class DropZoneService : IDropZoneService
    {
        private readonly ShareClient _shareClient;

        public DropZoneService(string connectionString, string shareName)
        {
            _shareClient = new ShareClient(connectionString, shareName);
        }

        /// <summary>
        /// Moves all .svs files from the archive directory to the main directory.
        /// </summary>
        public async Task MoveSvsFilesToMainFolderAsync()
        {
            ShareDirectoryClient archiveDir = _shareClient.GetDirectoryClient("archive");
            ShareDirectoryClient mainDir = _shareClient.GetRootDirectoryClient(); // qaorg is "main"

            await foreach (ShareFileItem item in archiveDir.GetFilesAndDirectoriesAsync())
            {
                if (!item.IsDirectory && item.Name.EndsWith(".svs", StringComparison.OrdinalIgnoreCase))
                {
                    ShareFileClient sourceFile = archiveDir.GetFileClient(item.Name);
                    ShareFileClient destFile = mainDir.GetFileClient(item.Name);

                    // Start the server-side copy
                    var copyResult = await destFile.StartCopyAsync(sourceFile.Uri);

                    // Wait until the copy status is success
                    ShareFileProperties props;
                    do
                    {
                        await Task.Delay(500);
                        props = await destFile.GetPropertiesAsync();
                    } while (props.CopyStatus == CopyStatus.Pending);

                    if (props.CopyStatus == CopyStatus.Success)
                    {
                        // Delete the source file
                        await sourceFile.DeleteIfExistsAsync();
                        Console.WriteLine($"Moved file: {item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Copy failed for file: {item.Name} - Status: {props.CopyStatus}");
                    }
                }
            }
        }
    }
}
