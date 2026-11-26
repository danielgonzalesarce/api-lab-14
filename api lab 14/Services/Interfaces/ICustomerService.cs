using api_lab_14.DTO.Request;
using api_lab_14.DTO.Response;

namespace api_lab_14.Services.Interfaces
{
    public interface ICustomerService
    {
        ResponseV1 Insert(CustomerInsertRequestV1 request);
        ResponseV1 Delete(CustomerDeleteRequestV1 request);
        ResponseV1 UpdateDocumentNumber(CustomerUpdateDocumentRequestV1 request);
    }
}
