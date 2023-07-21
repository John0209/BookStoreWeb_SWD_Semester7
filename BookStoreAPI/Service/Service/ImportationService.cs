using BookStoreAPI.Core.DiplayDTO;
using BookStoreAPI.Core.Interface;
using BookStoreAPI.Core.Model;
using Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using BookStoreAPI.Core.Function;
using System.Reflection.Metadata;
using OfficeOpenXml;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;


namespace Service.Service
{
    public class ImportationService : ControllerBase,IImportationService
    {
        IUnitOfWorkRepository _unit;
        private Importation m_import;
        IImportationDetailService m_detail;
        // 1: just created, 2: này như true - done, 3: deleted
        public ImportationService(IUnitOfWorkRepository unit,IImportationDetailService importationDetail)
        {
            _unit = unit;
            m_detail=importationDetail;
        }
        public async Task<bool> CreateImport(Importation import)
        {
            if (import != null)
            {
                //var m_list = await GetAllImport();
                import.Import_Id = Guid.NewGuid();
                import.Is_Import_Status = 1;
                await _unit.Importation.Add(import);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
        public async Task<FileResult> ExporteExcel(int month)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var convert = new ConvertNumberToText();
                var listImport = await _unit.Importation.GetAll();
                var listImportMonth = from l in listImport where l.Import_Date_Done.Month == month select l;
                var listExcel = await GetImportDetailByMonth(listImportMonth);

                string colorTitle = "#ffe699";
                string colorB5 = "#fce4d6";
                string colorContent = "#d0cece";
                var worksheet = package.Workbook.Worksheets.Add("Report");

                worksheet.Cells["D3"].Value = "BOOK IMPORT MANAGEMENT REPORT";
                worksheet.Cells["D3"].Style.Font.Size = 17;
                worksheet.Cells["D3"].Style.Font.Bold = true;
                worksheet.Cells["D3"].Style.Font.Color.SetColor(Color.Red);

                ////Cài đặt màu background
                ExcelRange target = worksheet.Cells["B3:G3"]; // Sử dụng ExcelRange của EPPlus
                target.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                target.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(colorTitle));
                target.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                //dòng time
                worksheet.Cells["D4"].Style.Font.Size = 15;
                worksheet.Cells["D4"].Style.Font.Bold = true;
                worksheet.Cells["D4"].Style.Font.Color.SetColor(Color.Green);
                worksheet.Cells["D4"].Value = "MONTH " + month + "/2023";
                // Cài đặt màu background và viền cho targetE4 (ExcelRange)
                ExcelRange targetE4 = worksheet.Cells["B4:G4"]; // Sử dụng ExcelRange của EPPlus
                targetE4.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                targetE4.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(colorB5));
                targetE4.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                targetE4.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                targetE4.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                // Cài đặt màu background và viền cho targetB5 (ExcelRange)
                ExcelRange targetB5 = worksheet.Cells["B5:G5"]; // Sử dụng ExcelRange của EPPlus
                targetB5.Style.Font.Color.SetColor(Color.DarkBlue);
                targetB5.Style.Font.Bold = true;
                targetB5.Style.Font.Size = 13;
                targetB5.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                targetB5.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(colorB5));
                targetB5.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                // Thiết lập border cho toàn bộ phạm vi targetB5
                targetB5.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                targetB5.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                targetB5.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                targetB5.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                // Thiết lập canh giữa theo chiều dọc và chiều ngang cho toàn bộ phạm vi
                targetB5.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                targetB5.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                // Cài đặt các giá trị và thuộc tính cho các ô trong targetB5
                worksheet.Cells["B5"].Value = "#";
                worksheet.Cells["C5"].Value = "Name Book";
                worksheet.Cells["D5"].Value = "Date Import";
                worksheet.Cells["E5"].Value = "Quantity";
                worksheet.Cells["F5"].Value = "Price";
                worksheet.Cells["G5"].Value = "Amount";

                // Setting column width cho các cột
                worksheet.Column(2).Width = 5; // Cột B
                worksheet.Column(3).Width = 20; // Cột C
                worksheet.Column(4).Width = 25; // Cột D
                worksheet.Column(5).Width = 13; // Cột E
                worksheet.Column(6).Width = 22; // Cột F
                worksheet.Column(7).Width = 22; // Cột G
                // Cài đặt nội dung từng import
                int number = 1;
                int i = 6;
                int quantityTotal = 0;
                float priceTotal = 0, amountTotal = 0;
                foreach (var x in listExcel)
                {
                    string formatA = x.price.ToString("N0") + " VND";
                    string formatP = x.amount.ToString("N0") + " VND";

                    // Sử dụng ExcelRange để cài đặt giá trị cho từng ô trong hàng
                    worksheet.Cells["B" + i].Value = number;
                    worksheet.Cells["C" + i].Value = x.NameBook;
                    worksheet.Cells["D" + i].Value = x.Date.ToString();
                    worksheet.Cells["E" + i].Value = x.quantity;
                    worksheet.Cells["F" + i].Value = formatA;
                    worksheet.Cells["G" + i].Value = formatP;

                    i++;
                    number++;
                    quantityTotal += x.quantity;
                    priceTotal += x.price;
                    amountTotal += x.amount;
                }
                // Set background cho các ô trong khoảng từ B6 đến G(i-1)
                ExcelRange target6 = worksheet.Cells["B6:G" + (i-1)];
                target6.Style.Font.Size = 12;
                target6.Style.Font.Color.SetColor(Color.Black);
                target6.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                target6.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(colorContent));
                target6.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                // Thiết lập border cho toàn bộ phạm vi targetB5
                target6.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                target6.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                target6.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                target6.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                // Thiết lập canh giữa theo chiều dọc và chiều ngang cho toàn bộ phạm vi
                target6.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                target6.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Setting total
                string formattedAmount = amountTotal.ToString("N0") + " VND";
                string formattedPrice = priceTotal.ToString("N0") + " VND";
                worksheet.Cells["C" + i].Value = "Total";
                worksheet.Cells["E" + i].Value = quantityTotal;
                worksheet.Cells["F" + i].Value = formattedPrice;
                worksheet.Cells["G" + i].Value = formattedAmount;

                // Convert số sang chữ
                worksheet.Cells["C" + (i + 1)].Value = "Total In Words";
                worksheet.Cells["D" + (i + 1)].Value = convert.NumberToText((double)amountTotal);

                // Cài đặt màu background cho hàng tổng cộng
                ExcelRange targetTotal = worksheet.Cells["B" + (i) + ":G" + (i)];
                targetTotal.Style.Font.Size = 13;
                targetTotal.Style.Font.Color.SetColor(Color.Red);
                targetTotal.Style.Font.Bold = true;
                targetTotal.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                targetTotal.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(colorTitle));
                targetTotal.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                // Thiết lập canh giữa theo chiều dọc và chiều ngang cho toàn bộ phạm vi
                targetTotal.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                targetTotal.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                // Thông số cho dòng "Total In Words"
                ExcelRange targetLast = worksheet.Cells["B" + (i+1) + ":G" + (i+1)];
                targetLast.Style.Font.Size = 10;
                targetLast.Style.Font.Color.SetColor(Color.Black);
                targetLast.Style.Font.Italic = true;
                targetLast.Style.Font.Bold = true;
                targetLast.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                targetLast.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(colorTitle));
                targetLast.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
               
                // Ghi nội dung tệp Excel vào MemoryStream
                var stream = new MemoryStream(package.GetAsByteArray());
                // Trả về tệp Excel dưới dạng FileResult
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
            }
        }

        private async Task<List<FileExcel>> GetImportDetailByMonth(IEnumerable<Importation> listImportMonth)
        {
           var listImport= new List<FileExcel>();
           foreach(var item in listImportMonth)
            {
                var list = await m_detail.GetImportDetailByImportId(item.Import_Id);
                foreach(var item2 in list)
                {
                    var import = new FileExcel();
                    // gắn biến vào file excel
                    import.Date = item.Import_Date_Done;
                    import.NameBook = item2.Book_Title;
                    import.quantity = item2.Import_Detail_Quantity;
                    import.price= item2.Import_Detail_Price;
                    import.amount=item2.Import_Detail_Amount;
                    listImport.Add(import);
                }
            }
           return listImport;
        }

        public async Task<bool> DeleteImport(Guid importId)
        {
            var m_update = _unit.Importation.SingleOrDefault(m_import, u => u.Import_Id==importId);
            if (m_update != null)
            {
                m_update.Is_Import_Status = 3;
                _unit.Importation.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
        public async Task<IEnumerable<Importation>> GetAllImport()
        {
            var result = await _unit.Importation.GetAll();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<DisplayImportationDTO>> GetDiplayImport()
        {
            var importList = await _unit.Importation.GetAll();
            var userList= await _unit.User.GetAll();
            var display = new List<DisplayImportationDTO>();
            foreach (var item in importList)
            {
                var import = new DisplayImportationDTO();
                import.Import_Id = item.Import_Id;
                import.Import_Quantity = item.Import_Quantity;
                import.Import_Amount=item.Import_Amount;
                import.Import_Date_Done = item.Import_Date_Done;
                import.Is_Import_Status= item.Is_Import_Status;
                import.User_Name = GetNameUser(item.User_Id, userList);
                display.Add(import);
            }
            if (display.Count < 1) return null;
            return display;
        }

        private string GetNameUser(Guid user_Id, IEnumerable<User> userList)
        {
            var userName = (from u in userList where u.User_Id == user_Id select u.User_Name).FirstOrDefault();
            return userName;
        }

        public Task<Book> GetImportById(Guid importId)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> UpdateStatusImport(Guid importId)
        {
            var m_update = _unit.Importation.SingleOrDefault(m_import, u => u.Import_Id== importId);
            if (m_update != null)
            {
                m_update.Is_Import_Status = 2;
                _unit.Importation.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<bool> RestoreImport(Guid importId)
        {
            var m_update = _unit.Importation.SingleOrDefault(m_import, u => u.Import_Id == importId);
            if (m_update != null)
            {
                m_update.Is_Import_Status = 2;
                _unit.Importation.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<bool> RemoveImport(Guid importId)
        {
            var import= await _unit.Importation.GetById(importId);
            var importDetailList= await _unit.ImportationDetail.GetAll();
            var listDetail= from i in importDetailList where i.Import_Id== importId select i;
            if (import != null)
            {
                if (listDetail.Count() > 0)
                {
                    foreach(var i in listDetail)
                    {
                        _unit.ImportationDetail.Delete(i);
                        _unit.Save();
                    }
                }
                _unit.Importation.Delete(import);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<Guid> GetImportIdJustCreated()
        {
           var listImport= await _unit.Importation.GetAll();
            var importId= (from i in listImport where i.Is_Import_Status==1 select i.Import_Id).FirstOrDefault();
            // sau khi lấy đước rồi thì update lại status import
            await UpdateStatusImport(importId);
            return importId;
        }
    }
}
