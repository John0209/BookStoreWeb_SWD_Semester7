using BookStoreAPI.Core.DiplayDTO;
using BookStoreAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.IService
{
    public interface IImportationDetailService
    {
        Task<bool> CreateImportDetail(ImportationDetail importDetail);
        Task<IEnumerable<ImportationDetail>> GetAllImportDetail();
        Task<IEnumerable<DiplayImportationDetailDTO>> GetImportDetailByImportId(Guid Import_Id);
        Task<IEnumerable<DiplayImportationDetailDTO>> GetDiplayImportDetail();
        Task<List<DiplayImportationDetailDTO>> SearchImport(string bookName);
        Task<bool> UpdateImportDetail(ImportationDetail importDetail);
        Task<bool> UpdateStatusRequest(Guid RequestId);
        //Task<bool> DeleteImportDetail(string importDetailId);
    }
}
