using System.ComponentModel.DataAnnotations;

namespace api_lab_14.DTO.Request
{
    public class ProductDeleteListRequestV1
    {
        [Required]
        [MinLength(1)]
        public List<ProductDeleteRequestV1> Products { get; set; } = new();
    }
}
