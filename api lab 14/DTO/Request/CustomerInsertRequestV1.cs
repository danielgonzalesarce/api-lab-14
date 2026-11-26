using System.ComponentModel.DataAnnotations;

namespace api_lab_14.DTO.Request
{
    public class CustomerInsertRequestV1
    {
        [Required]
        [StringLength(45)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(45)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(45)]
        public string DocumentNumber { get; set; } = string.Empty;
    }
}
