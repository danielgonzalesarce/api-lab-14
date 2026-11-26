using System.ComponentModel.DataAnnotations;

namespace api_lab_14.DTO.Request
{
    public class CustomerUpdateDocumentRequestV1
    {
        public int Id { get; set; }

        [Required]
        [StringLength(45)]
        public string DocumentNumber { get; set; } = string.Empty;
    }
}
