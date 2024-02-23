using AOGSystem.Domain.Invoices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOGSystem.Domain.SOA;
using DocumentFormat.OpenXml;
using AOGSystem.Domain.General;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.Json.Serialization;

namespace AOGSystem.Application.SOA.Commands
{
    public class ImportInvoiceListCommandHandler : IRequestHandler<ImportInvoiceListCommand, List<JObject>>
    {
        private readonly IInvoiceListRepository _invoiceListRepository;
        private readonly IVendorRepository _vendorRepository;
        public ImportInvoiceListCommandHandler(IInvoiceListRepository invoiceListRepository, IVendorRepository vendorRepository)
        {
            _invoiceListRepository = invoiceListRepository;
            _vendorRepository = vendorRepository;
        }

        public async Task<List<JObject>> Handle(ImportInvoiceListCommand request, CancellationToken cancellationToken)
        {
            var response = new List<JObject>();
            int rowCount = 0;
            int importedInvoices = 0;
            int updatedInvoices = 0;

            var invoiceLists = new List<InvoiceList>();

            var vendor = await _vendorRepository.GetVendorSOAByIDAsync(request.VendorId);
            if (vendor == null)
            {
                var errorObject = new JObject { { "error", "Vendor Doesnot exist." } };
                response.Add(errorObject);
                return response;
            }
            try
            {
                using (var stream = File.OpenRead(request.FileDirectory))
                {
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                            if (!IsWriteImportFileFormat(reader))
                            {
                                var errorObject = new JObject { { "file_format_error", "Used excel format is not correct." } };
                                response.Add(errorObject);
                                return response;
                            }
                        while (reader.Read())
                        {
                            if (rowCount >= 0)
                            {
                                var invoiceNo = GetStringFromCell(reader, 0);
                                var poNo = GetStringFromCell(reader, 1);
                                var invoiceDate = GetDateFromCell(reader, 2);
                                var dueDate = GetDateFromCell(reader, 3);
                                var amount = GetDoubleFromCell(reader, 4);
                                var currency = GetStringFromCell(reader, 5);
                                var underFollowup = GetStringFromCell(reader, 6);
                                var buyerName = GetStringFromCell(reader, 7);
                                var tLName = GetStringFromCell(reader, 8);
                                var managerName = GetStringFromCell(reader, 9);
                                var status = GetStringFromCell(reader, 10);

                                if(invoiceNo != null && invoiceNo != "") 
                                { 
                                    var exists = await _invoiceListRepository.GetSOAInvoiceListByInvoiceNoAsync(invoiceNo);
                                    if (exists != null)
                                    {
                                        exists.SetInvoiceNo(invoiceNo);
                                        exists.SetPONo(poNo);
                                        exists.SetInvoiceDate(invoiceDate);
                                        exists.SetDueDate(dueDate); 
                                        exists.SetAmount(amount);
                                        exists.SetCurrency(currency);
                                        exists.SetUnderForllowup(underFollowup);
                                        exists.SetBuyerName(buyerName);
                                        exists.SetTLName(tLName);
                                        exists.SetManagerName(managerName);
                                        exists.SetStatus(status);
                                        exists.UpdatedAT = DateTime.Now;
                                        exists.UpdatedBy = request.CreatedBy;

                                        updatedInvoices++;
                                        invoiceLists.Add(exists);

                                        var errorObject = new JObject { { "Update", $"Data for Invoice number '{invoiceNo}' updated" } };
                                        response.Add(errorObject);
                                    }
                                    else
                                    {
                                        var invoice = new InvoiceList(invoiceNo, poNo, invoiceDate, dueDate, amount, currency, underFollowup, buyerName, tLName, managerName, status);
                                        invoice.CreatedAT = DateTime.Now;
                                        invoice.CreatedBy = request.CreatedBy;
                                        importedInvoices++;
                                        invoiceLists.Add(invoice);
                                        vendor.AddInvoiceList(invoice);
                                    }
                                }
                            }

                            rowCount++;
                        }
                    }
                    response.Add(new JObject { { "success_message", $"{importedInvoices} Invoices imported and {updatedInvoices} Invoices updated sucessfuly" }, { "result", JArray.FromObject(invoiceLists) } });
                    var result = await _vendorRepository.SaveChangesAsync();
                    if (result == 0)
                        response.Add(new JObject { { "save_error", "something went wrong when saving invoice lists" } });

                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred: {ex.Message}";
                var errorObject = new JObject { { "error", errorMessage } };
                response.Add(errorObject);
            }

            return response;

        }

        

        private string GetStringFromCell(IExcelDataReader reader, int columnIndex)
        {
            return reader.IsDBNull(columnIndex) ? string.Empty : reader.GetValue(columnIndex).ToString();
        }

        private DateTime GetDateFromCell(IExcelDataReader reader, int columnIndex)
        {
            return reader.IsDBNull(columnIndex) ? DateTime.Now : reader.GetDateTime(columnIndex);
        }
        private double GetDoubleFromCell(IExcelDataReader reader, int columnIndex)
        {
            if (reader.IsDBNull(columnIndex))
                return 0.0;
            else
            {
                double doubleValue;
                if (double.TryParse(reader.GetValue(columnIndex).ToString(), out doubleValue))
                    return doubleValue;
                else
                    return 0.0;
            }
        }

        private bool IsWriteImportFileFormat(IExcelDataReader reader)
        {
            string[] expectedHeaders = { "Invoice No", "PO No", "Invoice Date", "Due Date", "Amount", "Currency", "Under Followup", "Buyer Name", "TL Name", "Manager Name", "Status" };

            var actualHeaders = new List<string>();
            reader.Read();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if(reader.GetString(i) != null)
                    actualHeaders.Add(reader.GetString(i));
            }
            bool headersMatch = actualHeaders.SequenceEqual(expectedHeaders);
            return headersMatch;
        }

    }

    public class ImportInvoiceListCommand : IRequest<List<JObject>>
    {
        public Guid VendorId { get; set; }
        public IFormFile File { get; set; }
        public Guid? CodesetId { get; set; }
        public string? FileDirectory { get; set; }
        public string? Extension { get; set; }

        [JsonIgnore]
        public string? CreatedBy { get; private set; }
        public void SetCreatedBy(string createdBy) { CreatedBy = createdBy; }
    }
}
