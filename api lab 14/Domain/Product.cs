namespace api_lab_14.Domain
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public List<Detail> Details { get; set; } = new();
    }
}
