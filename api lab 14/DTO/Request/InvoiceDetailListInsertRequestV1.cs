using System.ComponentModel.DataAnnotations;

namespace api_lab_14.DTO.Request
{
    public class InvoiceDetailListInsertRequestV1
    {
        public int IdInvoice { get; set; }

        [Required]
        [MinLength(1)]
        public List<DetailItemRequestV1> Details { get; set; } = new();
    }
}
