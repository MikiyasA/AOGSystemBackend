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
using DocumentFormat.OpenXml.Drawing.Charts;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace AOGSystem.Application.SOA.Commands
{
    public class ImportInvoiceListCommandHandler : IRequestHandler<ImportInvoiceListCommand, JObject>
    {
        private readonly IInvoiceListRepository _invoiceListRepository;
        private readonly IVendorRepository _vendorRepository;
        public ImportInvoiceListCommandHandler(IInvoiceListRepository invoiceListRepository, IVendorRepository vendorRepository)
        {
            _invoiceListRepository = invoiceListRepository;
            _vendorRepository = vendorRepository;
        }

        public async Task<JObject> Handle(ImportInvoiceListCommand request, CancellationToken cancellationToken)
        {
            
            var response = new JObject();
            response["error"] = new JArray();
            response["success"] = new JArray();
            response["summery"] = new JArray();
            var responseData = new JObject();

            int rowCount = 0;
            int importedInvoices = 0;
            int updatedInvoices = 0;

            var invoiceLists = new List<InvoiceList>();

            var vendor = await _vendorRepository.GetActiveVendorSOAByIDAsync(request.VendorId);
            if (vendor == null)
            {
                ((JArray)response["error"]).Add("Active Vendor Does not exist.");
                responseData["data"] = response;
                return responseData;
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
                            ((JArray)response["error"]).Add("Used excel format is not correct.");
                            responseData["data"] = response;
                            return responseData;
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
                                var paymentProcessDate = GetDateFromCell(reader, 7);
                                var popDate = GetDateFromCell(reader, 8);
                                var popReference = GetStringFromCell(reader, 9);
                                var chargeType = GetStringFromCell(reader, 10);
                                var buyerName = GetStringFromCell(reader, 11);
                                var tLName = GetStringFromCell(reader, 12);
                                var managerName = GetStringFromCell(reader, 13);
                                var status = GetStringFromCell(reader, 14);

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
                                        exists.SetPaymentProcessedDate(paymentProcessDate);
                                        exists.SetPOPDate(popDate);
                                        exists.SetPOPReference(popReference);
                                        exists.SetChargeTyoe(chargeType);
                                        exists.SetBuyerName(buyerName);
                                        exists.SetTLName(tLName);
                                        exists.SetManagerName(managerName);
                                        exists.SetStatus(status);
                                        exists.UpdatedAT = DateTime.Now;
                                        exists.UpdatedBy = request.CreatedBy;

                                        updatedInvoices++;
                                        invoiceLists.Add(exists);

                                        ((JArray)response["success"]).Add($"Data for Invoice number '{invoiceNo}' updated");
                                    }
                                    else
                                    {
                                        var invoice = new InvoiceList(invoiceNo, poNo, invoiceDate, dueDate, amount, currency, underFollowup, paymentProcessDate, popDate, popReference, chargeType, buyerName,
                                            tLName, managerName, status);
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
                    ((JArray)response["summery"]).Add($"{importedInvoices} Invoices imported and {updatedInvoices} Invoices updated successfully");
                    //((JArray)response["success"]).Add(new JObject(invoiceLists));
                    var result = await _vendorRepository.SaveChangesAsync();
                    if (result == 0)
                        ((JArray)response["error"]).Add($"something went wrong when saving invoice lists");
                }
            }
            catch (Exception ex)
            {
                ((JArray)response["error"]).Add($"An error occurred: {ex.Message}");
            }

            responseData["data"] = response;
            return responseData;

        }

        

        private string GetStringFromCell(IExcelDataReader reader, int columnIndex)
        {
            return reader.IsDBNull(columnIndex) ? string.Empty : reader.GetValue(columnIndex).ToString();
        }

        private DateTime GetDateFromCell(IExcelDataReader reader, int columnIndex)
        {
            if (reader.IsDBNull(columnIndex))
            {
                return DateTime.Now; // Or any default value you prefer
            }
            else
            {
                if (reader.GetFieldType(columnIndex) == typeof(DateTime))
                {
                    return reader.GetDateTime(columnIndex);
                }
                else
                {
                    string cellValue = reader.GetString(columnIndex);
                    if (DateTime.TryParse(cellValue, out DateTime date))
                        return date;
                    else
                        throw new ArgumentException($"Unable to parse '{cellValue}' as a valid DateTime.");
                }
            }
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
            string[] expectedHeaders = { "Invoice No", "PO No", "Invoice Date", "Due Date", "Amount", "Currency", "Under Followup", "Payment Processed Date", "POP Date", "POP Reference", "Charge Type",
                    "Buyer Name", "TL Name", "Manager Name", "Status"
                 };

            var actualHeaders = new List<string>();
            reader.Read();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetString(i) != null)
                    actualHeaders.Add(reader.GetString(i).ToLower());
            }

            var expectedHeadersLower = expectedHeaders.Select(header => header.ToLower()).ToArray();

            bool headersMatch = actualHeaders.SequenceEqual(expectedHeadersLower);
            return headersMatch;
        }

    }

    public class ImportInvoiceListCommand : IRequest<JObject>
    {
        public Guid VendorId { get; set; }
        public IFormFile? File { get; set; }
        public Guid? CodesetId { get; set; }
        public string? FileDirectory { get; set; }
        public string? Extension { get; set; }

        [JsonIgnore]
        public Guid? CreatedBy { get; private set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }
    }
}
