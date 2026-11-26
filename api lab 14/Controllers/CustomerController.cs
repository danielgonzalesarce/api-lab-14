using api_lab_14.DTO.Request;
using api_lab_14.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_lab_14.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("insert")]
        public IActionResult Insert([FromBody] CustomerInsertRequestV1 request)
        {
            var response = _customerService.Insert(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] CustomerDeleteRequestV1 request)
        {
            var response = _customerService.Delete(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("update-document")]
        public IActionResult UpdateDocument([FromBody] CustomerUpdateDocumentRequestV1 request)
        {
            var response = _customerService.UpdateDocumentNumber(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
