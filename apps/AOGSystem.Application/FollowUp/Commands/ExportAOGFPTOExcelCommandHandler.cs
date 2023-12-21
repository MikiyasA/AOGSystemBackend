using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Commands
{
    public class ExportAOGFPTOExcelCommandHandler : IRequestHandler<ExportAOGFPTOExcelCommand, byte[]>
    {
        private readonly IAOGFollowUpRepository _AOGFollowUpRepository;
        private readonly IPartRepository _prtRepository;
        public ExportAOGFPTOExcelCommandHandler(IAOGFollowUpRepository aOGFollowUpRepository,
            IPartRepository partRepository)
        {
            _AOGFollowUpRepository = aOGFollowUpRepository;
            _prtRepository = partRepository;
        }


        internal void ExportExcelFile(bool isFistColumn, string title, List<AOGFollowUp> data, int rowStart, IXLWorksheet worksheet )
        {

            var mergedRangeTitle = worksheet.Range(rowStart, 1, rowStart, 12);
            mergedRangeTitle.Merge();
            worksheet.Cell(rowStart, 1).Value = title;

            var mergedRangeDate = worksheet.Range(rowStart, 13, rowStart, 16);
            mergedRangeDate.Merge();

            if (isFistColumn)
            {
                var today = DateTime.Now;

                string date = today.ToString("dd-MMM-yyyy");
                int hour = int.Parse(today.ToString("HH"));

                string shift = hour < 3 ? "Evening" : hour < 9 ? "Night" : hour < 18 ? "Day" : "";

                worksheet.Cell(rowStart, 13).Value = $"{date} {shift} Shift";
            }

            worksheet.Cell(rowStart, 1).Style.Fill.BackgroundColor = XLColor.LightGreen;
            worksheet.Cell(rowStart, 13).Style.Fill.BackgroundColor = XLColor.LightGreen;
            worksheet.Cell(rowStart, 1).Style.Font.FontSize = 14;
            worksheet.Cell(rowStart, 13).Style.Font.FontSize = 14;
            worksheet.Cell(rowStart, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(rowStart, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Cell(rowStart, 13).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Row(rowStart).Height = 30;



            var headers = new List<string>
                {
                    "Item#", "RID", "Request Date", "A/C", "Tail No", "Customer", "PN", "Description", "Stock No", "PO", "Type", "Quantity", "Vendor",
                    "Edd", "AWB", "Remark"
                };

            for (int i = 1; i <= headers.Count; i++)
            {
                worksheet.Cell(rowStart+1, i).Value = headers[i - 1];
                worksheet.Cell(rowStart+1, i).Style.Font.SetBold(true);

                //worksheet.Cell(2, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(rowStart+1, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Range(rowStart, 1, rowStart+1, i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                int row = rowStart+2;
                foreach (var item in data)
                {
                    // Populate the cells with data
                    var part = _prtRepository.GetPartByIDAsync(item.PartId);
                    worksheet.Cell(row, 1).Value = data.IndexOf(item)+1;
                    worksheet.Cell(row, 2).Value = item.RID;
                    worksheet.Cell(row, 3).Value = item.RequestDate;
                    worksheet.Cell(row, 4).Value = item.AirCraft;
                    worksheet.Cell(row, 5).Value = item.TailNo;
                    worksheet.Cell(row, 6).Value = item.Customer;
                    worksheet.Cell(row, 7).Value = part.Result.PartNumber;
                    worksheet.Cell(row, 8).Value = part.Result.Description;
                    worksheet.Cell(row, 9).Value = part.Result.StockNo;
                    worksheet.Cell(row, 10).Value = item.PONumber;
                    worksheet.Cell(row, 11).Value = item.OrderType;
                    worksheet.Cell(row, 12).Value = $"{item.Quantity} {item.UOM}";
                    worksheet.Cell(row, 13).Value = item.Vendor;
                    worksheet.Cell(row, 14).Value = item.EDD;
                    worksheet.Cell(row, 15).Value = item.AWBNo;
                    worksheet.Cell(row, 16).Value = item.Remarks.FirstOrDefault().Message;

                    // format row
                    worksheet.Cell(row, 3).Style.DateFormat.Format = "dd-MMM-yy";
                    worksheet.Cell(row, 14).Style.DateFormat.Format = "dd-MMM-yy";
                    worksheet.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    //worksheet.Row(row).Height = 18; // Tb uncommented if row hieght is needed

                    worksheet.Cell(row, 8).Style.Alignment.WrapText = true;
                    worksheet.Cell(2, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;


                    // format row and column
                    worksheet.Cell(row, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Cell(row, i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    row++;
                }
                worksheet.Column(i).AdjustToContents();
                worksheet.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Column(8).Width = 30;
                worksheet.Column(16).Width = 30;

            }
            worksheet.Row(rowStart+1).Height = 22;
            worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            worksheet.PageSetup.FitToPages(1, 50); // Scale to fit on one page both vertically and horizontally


        }


        public async Task<byte[]> Handle(ExportAOGFPTOExcelCommand request, CancellationToken cancellationToken)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Data");

                var data = await _AOGFollowUpRepository.GetAllActiveFollowUpAsync();

                var outStation = data.FindAll(x => x.WorkLocation == "Out Station" && x.Status != "Under Receiving");
                var homeBase = data.FindAll(x => x.WorkLocation == "Home Base" && x.Status != "Under Receiving");
                var tool = data.FindAll(x => x.WorkLocation == "Tool" && x.Status != "Under Receiving");

                var underRecieving = data.FindAll(x => x.Status == "Under Receiving");

                ExportExcelFile(true, "Out Station Follow-up", outStation, 1, worksheet);
                ExportExcelFile(false, "Home Base Follow-up", homeBase, outStation.Count + 3, worksheet);
                ExportExcelFile(false, "Tool Follow-up", tool, outStation.Count + homeBase.Count + 5, worksheet);
                ExportExcelFile(false, "Under Receiving", underRecieving, outStation.Count + homeBase.Count + tool.Count + 7, worksheet);


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }

    public class ExportAOGFPTOExcelCommand : IRequest<byte[]>
    {

    }



}


// ToBe Deleted
//var worksheet = workbook.Worksheets.Add("Data");

//var mergedRangeTitle = worksheet.Range("A1:K1");
//mergedRangeTitle.Merge();

//var mergedRangeDate = worksheet.Range("L1:O1");
//mergedRangeDate.Merge();

//worksheet.Cell(1, 1).Value = "mergedRangeTitle";
//worksheet.Cell(1, 12).Value = "mergedRangeDate";

//worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGreen;
//worksheet.Cell(1, 12).Style.Fill.BackgroundColor = XLColor.LightGreen;
//worksheet.Cell(1, 1).Style.Font.FontSize = 16;
//worksheet.Cell(1, 12).Style.Font.FontSize = 16;
//worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
//worksheet.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
//worksheet.Cell(1, 12).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
//worksheet.Row(1).Height = 30;


//var data = await _AOGFollowUpRepository.GetAllActiveFollowUpAsync();

//var headers = new List<string>
//{
//    "RID", "Request Date", "A/C", "Tail No", "Customer", "PN", "Description", "Stock No", "PO", "Type", "Quantity", "Vendor",
//    "Edd", "AWB", "Remark"
//};

//for (int i = 1; i <= headers.Count; i++)
//{
//    worksheet.Cell(2, i).Value = headers[i - 1];
//    worksheet.Cell(2, i).Style.Font.SetBold(true);

//    //worksheet.Cell(2, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
//    worksheet.Cell(2, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

//    int row = 3; 
//    foreach (var item in data)
//    {
//        // Populate the cells with data
//        var part = _prtRepository.GetPartByIDAsync(item.PartId);
//        worksheet.Cell(row, 1).Value = item.RID;
//        worksheet.Cell(row, 2).Value = item.RequestDate;
//        worksheet.Cell(row, 3).Value = item.AirCraft;
//        worksheet.Cell(row, 4).Value = item.TailNo;
//        worksheet.Cell(row, 5).Value = item.Customer;
//        worksheet.Cell(row, 6).Value = part.Result.PartNumber;
//        worksheet.Cell(row, 7).Value = part.Result.Description;
//        worksheet.Cell(row, 8).Value = part.Result.StockNo;
//        worksheet.Cell(row, 9).Value = item.PONumber;
//        worksheet.Cell(row, 10).Value = item.OrderType;
//        worksheet.Cell(row, 11).Value = $"{item.Quantity} {item.UOM}";
//        worksheet.Cell(row, 12).Value = item.Vendor;
//        worksheet.Cell(row, 13).Value = item.EDD;
//        worksheet.Cell(row, 14).Value = item.AWBNo;
//        worksheet.Cell(row, 15).Value = item.Remarks.FirstOrDefault().Message;

//        // format row
//        worksheet.Cell(row, 2).Style.DateFormat.Format = "dd-MMM-yy";
//        worksheet.Cell(row, 13).Style.DateFormat.Format = "dd-MMM-yy";
//        worksheet.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

//        worksheet.Row(row).Height = 18;

//        worksheet.Cell(row, 7).Style.Alignment.WrapText = true;


//        // format row and column
//        worksheet.Cell(row, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
//        worksheet.Cell(row, i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

//        row++;
//    }
//    worksheet.Column(i).AdjustToContents();
//    worksheet.Column(7).Width = 30;
//    worksheet.Column(15).Width = 30;

//}
//worksheet.Row(2).Height = 22;