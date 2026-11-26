using api_lab_14.DTO.Request;
using api_lab_14.DTO.Response;

namespace api_lab_14.Services.Interfaces
{
    public interface IInvoiceService
    {
        ResponseV1 Insert(InvoiceInsertRequestV1 request);
        ResponseV1 InsertListByCustomer(InvoiceListInsertRequestV1 request);
        ResponseV1 InsertDetails(InvoiceDetailListInsertRequestV1 request);
    }
}
