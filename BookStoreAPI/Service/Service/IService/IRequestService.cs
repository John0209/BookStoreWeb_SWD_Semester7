using BookStoreAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.IService
{
    public interface IRequestService
    {
        Task<bool> CreateRequest(BookingRequest request, bool status);
        Task<IEnumerable<BookingRequest>> GetAllRequest();
        Task<BookingRequest> GetRequestById(Guid requestId);
        Task<bool> UpdateRequest(BookingRequest request);
        Task<bool> UpdateRequestUnDone(Guid requestId, string note);
        Task<bool> DeleteRequest(Guid requestId);
        Task<bool> RemoveRequest(Guid requestId);
        Task<bool> RestoreRequest(Guid requestId);
    }
}
