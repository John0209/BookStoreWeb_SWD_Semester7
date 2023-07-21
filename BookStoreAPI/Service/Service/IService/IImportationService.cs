using BookStoreAPI.Core.DiplayDTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.IService
{
   public interface IImportationService
    {
        Task<bool> CreateImport(Importation import);
        Task<IEnumerable<Importation>> GetAllImport();
        Task<IEnumerable<DisplayImportationDTO>> GetDiplayImport();
        Task<Book> GetImportById(Guid importId);
        Task<bool> DeleteImport(Guid importId);
        Task<bool> RestoreImport(Guid importId);
        Task<bool> RemoveImport(Guid importId);
        Task<Guid> GetImportIdJustCreated();
        Task<FileResult> ExporteExcel(int month);
    }
}
