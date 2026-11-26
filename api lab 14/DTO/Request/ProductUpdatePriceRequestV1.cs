using System.ComponentModel.DataAnnotations;

namespace api_lab_14.DTO.Request
{
    public class ProductUpdatePriceRequestV1
    {
        public int Id { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }
    }
}
