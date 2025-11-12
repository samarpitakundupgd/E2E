using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ETests.Interfaces
{
    /// <summary>
    /// Interface for interacting with the FourDPathAPI
    /// </summary>
    public interface ICaseRequestService
    {
        Task<HttpResponseMessage> PostCaseRequestAsync(string caseRequestJson);
    }
}
