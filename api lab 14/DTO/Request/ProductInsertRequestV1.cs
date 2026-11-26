using System.ComponentModel.DataAnnotations;

namespace api_lab_14.DTO.Request
{
    public class ProductInsertRequestV1
    {
        [Required]
        [StringLength(45)]
        public string Name { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public double Price { get; set; }
    }
}
