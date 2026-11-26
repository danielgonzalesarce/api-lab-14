using api_lab_14.DTO.Request;
using api_lab_14.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_lab_14.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("insert")]
        public IActionResult Insert([FromBody] ProductInsertRequestV1 request)
        {
            var response = _productService.Insert(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] ProductDeleteRequestV1 request)
        {
            var response = _productService.Delete(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("update-price")]
        public IActionResult UpdatePrice([FromBody] ProductUpdatePriceRequestV1 request)
        {
            var response = _productService.UpdatePrice(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("delete-list")]
        public IActionResult DeleteList([FromBody] ProductDeleteListRequestV1 request)
        {
            var response = _productService.DeleteList(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
