using api_lab_14.DTO.Request;
using api_lab_14.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_lab_14.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("insert")]
        public IActionResult Insert([FromBody] InvoiceInsertRequestV1 request)
        {
            var response = _invoiceService.Insert(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("insert-list")]
        public IActionResult InsertList([FromBody] InvoiceListInsertRequestV1 request)
        {
            var response = _invoiceService.InsertListByCustomer(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("insert-details")]
        public IActionResult InsertDetails([FromBody] InvoiceDetailListInsertRequestV1 request)
        {
            var response = _invoiceService.InsertDetails(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
