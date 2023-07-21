using BookStoreAPI.Core.DiplayDTO;
using BookStoreAPI.Core.Interface;
using BookStoreAPI.Core.Model;
using Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ImportationDetailService : IImportationDetailService
    {
        IUnitOfWorkRepository _unit;
        IBookRepository _book;
        IImageBookRepository _imageBook;
        public ImportationDetailService(IUnitOfWorkRepository unitOfWorkRepository, IBookRepository book, 
            IImageBookRepository imageBook)
        {
            _unit = unitOfWorkRepository;
            _book = book;
            _imageBook = imageBook;
        }
        public async Task<bool> CreateImportDetail(ImportationDetail importDetail)
        {
            if (importDetail != null)
            {
                importDetail.Import_Detail_Id = Guid.NewGuid();
                await UpdateQuantityBook(importDetail.Book_Id, importDetail.Import_Detail_Quantity);
                await _unit.ImportationDetail.Add(importDetail);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        private async Task UpdateQuantityBook(Guid book_Id, int import_Detail_Quantity)
        {
            var book=await _unit.Books.GetById(book_Id);
            if(book != null)
            {
                book.Book_Quantity += import_Detail_Quantity;
                _unit.Books.Update(book);
                _unit.Save();
            }
        }

        public async Task<IEnumerable<ImportationDetail>> GetAllImportDetail()
        {
            var result = await _unit.ImportationDetail.GetAll();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public async Task<IEnumerable<DiplayImportationDetailDTO>> GetDiplayImportDetail()
        {
            var importList= await _unit.ImportationDetail.GetAll();
            var display = new List<DiplayImportationDetailDTO>();
            // get filed để display
            display =await GetDisplay(display, importList);
            if (display.Count < 1) return null;
            return display;

        }

        private async Task<List<DiplayImportationDetailDTO>> GetDisplay(List<DiplayImportationDetailDTO> display, IEnumerable<ImportationDetail> importList)
        {
            var bookList = await _unit.Books.GetAll();
            var image = await _unit.Images.GetAll();
            foreach (var item in importList)
            {
                var import = new DiplayImportationDetailDTO();
                import.Import_Detail_Id = item.Import_Detail_Id;
                import.Import_Id = item.Import_Id;
                import.Import_Detail_Quantity = item.Import_Detail_Quantity;
                import.Import_Detail_Price = item.Import_Detail_Price;
                import.Import_Detail_Amount = item.Import_Detail_Amount;
                import.Book_Title = GetTitle(item.Book_Id, bookList);
                import.Image_URL = GetUrl(item.Book_Id, image);
                display.Add(import);
            }
            return display;
        }

        private string GetUrl(Guid book_Id, IEnumerable<ImageBook> image)
        {
            var url = (from b in image where b.Book_Id == book_Id select b.Image_URL).FirstOrDefault();
            return url;
        }

        private string GetTitle(Guid book_Id, IEnumerable<Book> bookList)
        {
            var title= (from b in bookList where b.Book_Id== book_Id select b.Book_Title).FirstOrDefault();
            return title;
        }

        public async Task<List<DiplayImportationDetailDTO>> SearchImport(string bookName)
        {
            var books = await _unit.Books.GetAll();
            var listAllImportDetail = await GetAllImportDetail();
            var listAllImport= await _unit.Importation.GetAll();
            //get all import when imporst status ==true
            var importations = (from d in listAllImportDetail
                                join i in listAllImport on d.Import_Id equals i.Import_Id
                                where i.Is_Import_Status == 2
                                select d);
            // lấy nhựng book có name cẩn search
            var bookIdList = from b in books where (b.Book_Title.ToLower().Trim().Contains(bookName.ToLower().Trim())) select b;
            // lấy inventory có chứa những book có id cần search
            var importationList = (bookIdList.Join(importations, b => b.Book_Id, i => i.Book_Id, (b, i) => { return i; }));
            //lấy thông tin để show ra screen
            var listDisplay = new List<DiplayImportationDetailDTO>();
            listDisplay = await GetDisplay(listDisplay, importationList);
            if (listDisplay.Count() > 0) return listDisplay;
            return null;
        }

        public Task<bool> UpdateImportDetail(ImportationDetail importDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateStatusRequest(Guid RequestId)
        {
            var request= await _unit.Request.GetById(RequestId);
            if(request != null)
            {
                request.Request_Date_Done= DateTime.Now;
                request.Is_Request_Status = 2;
                _unit.Request.Update(request);
               var result= _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<IEnumerable<DiplayImportationDetailDTO>> GetImportDetailByImportId(Guid Import_Id)
        {
            var listImportDetail = await GetAllImportDetail();
            var importDetail = from i in listImportDetail where i.Import_Id == Import_Id select i;
            var display = new List<DiplayImportationDetailDTO>();
            // get filed để display
            display = await GetDisplay(display, importDetail);
            if (display.Count < 1) return null;
            return display;
        }
    }
}
