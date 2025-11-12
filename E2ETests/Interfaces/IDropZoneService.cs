using System.Collections.Generic;

namespace E2ETests.Interfaces
{
    /// <summary>
    /// Interface for the drop zone service
    /// </summary>
    public interface IDropZoneService
    {

        /// <summary>
        /// Moves all .svs files from the archive directory to the main directory.
        /// </summary>
        public Task MoveSvsFilesToMainFolderAsync();

    }
}
