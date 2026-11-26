using System.ComponentModel.DataAnnotations;

namespace api_lab_14.DTO.Request
{
    public class InvoiceListInsertRequestV1
    {
        public int IdCustomer { get; set; }

        [Required]
        [MinLength(1)]
        public List<InvoiceItemRequestV1> Invoices { get; set; } = new();
    }
}
