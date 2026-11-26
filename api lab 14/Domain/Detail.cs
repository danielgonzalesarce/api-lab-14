namespace api_lab_14.Domain
{
    public class Detail
    {
        public int DetailId { get; set; }
        public int ProductId { get; set; }
        public int InvoiceId { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public double SubTotal { get; set; }
        public Product? Product { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
