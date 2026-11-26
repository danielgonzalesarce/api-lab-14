using System.ComponentModel.DataAnnotations;

namespace api_lab_14.DTO.Request
{
    public class DetailItemRequestV1
    {
        public int IdProduct { get; set; }

        [Range(1, int.MaxValue)]
        public int Amount { get; set; }
    }
}
