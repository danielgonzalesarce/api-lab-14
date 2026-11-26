using api_lab_14.DTO.Request;
using api_lab_14.DTO.Response;

namespace api_lab_14.Services.Interfaces
{
    public interface IProductService
    {
        ResponseV1 Insert(ProductInsertRequestV1 request);
        ResponseV1 Delete(ProductDeleteRequestV1 request);
        ResponseV1 UpdatePrice(ProductUpdatePriceRequestV1 request);
        ResponseV1 DeleteList(ProductDeleteListRequestV1 request);
    }
}
