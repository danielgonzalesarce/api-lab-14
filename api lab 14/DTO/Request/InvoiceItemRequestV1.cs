using System.ComponentModel.DataAnnotations;

namespace api_lab_14.DTO.Request
{
    public class InvoiceItemRequestV1
    {
        public DateTime Date { get; set; }

        [Required]
        [StringLength(45)]
        public string InvoiceNumber { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public double Total { get; set; }
    }
}
