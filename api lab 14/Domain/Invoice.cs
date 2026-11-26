namespace api_lab_14.Domain
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public double Total { get; set; }
        public Customer? Customer { get; set; }
        public List<Detail> Details { get; set; } = new();
    }
}
