using api_lab_14.Domain;
using api_lab_14.DTO.Request;
using api_lab_14.DTO.Response;
using api_lab_14.Infrastructure;
using api_lab_14.Services.Interfaces;

namespace api_lab_14.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;

        public InvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ResponseV1 Insert(InvoiceInsertRequestV1 request)
        {
            var customerExists = _context.Customers.Any(c => c.CustomerId == request.IdCustomer);
            if (!customerExists)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "El cliente no existe."
                };
            }

            var invoiceExists = _context.Invoices.Any(i => i.InvoiceNumber == request.InvoiceNumber);
            if (invoiceExists)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "Ya existe una factura con ese número."
                };
            }

            var invoice = new Invoice
            {
                CustomerId = request.IdCustomer,
                Date = request.Date,
                InvoiceNumber = request.InvoiceNumber,
                Total = request.Total
            };

            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return new ResponseV1
            {
                Success = true,
                Message = "Factura insertada correctamente.",
                Id = invoice.InvoiceId
            };
        }

        public ResponseV1 InsertListByCustomer(InvoiceListInsertRequestV1 request)
        {
            var customerExists = _context.Customers.Any(c => c.CustomerId == request.IdCustomer);
            if (!customerExists)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "El cliente no existe."
                };
            }

            var invoiceNumbers = request.Invoices.Select(i => i.InvoiceNumber).ToList();
            if (invoiceNumbers.Distinct().Count() != invoiceNumbers.Count)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "La lista contiene números de factura repetidos. No se insertó ninguna."
                };
            }

            var existingNumbers = _context.Invoices
                .Where(i => invoiceNumbers.Contains(i.InvoiceNumber))
                .Select(i => i.InvoiceNumber)
                .ToList();

            if (existingNumbers.Count > 0)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "Uno o más números de factura ya existen. No se insertó ninguna."
                };
            }

            var invoices = request.Invoices.Select(item => new Invoice
            {
                CustomerId = request.IdCustomer,
                Date = item.Date,
                InvoiceNumber = item.InvoiceNumber,
                Total = item.Total
            }).ToList();

            _context.Invoices.AddRange(invoices);
            _context.SaveChanges();

            return new ResponseV1
            {
                Success = true,
                Message = $"Se insertaron {invoices.Count} facturas."
            };
        }

        public ResponseV1 InsertDetails(InvoiceDetailListInsertRequestV1 request)
        {
            var invoice = _context.Invoices.FirstOrDefault(i => i.InvoiceId == request.IdInvoice);
            if (invoice == null)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "La factura no existe."
                };
            }

            var productIds = request.Details.Select(d => d.IdProduct).Distinct().ToList();
            var products = _context.Products.Where(p => productIds.Contains(p.ProductId)).ToList();
            if (products.Count != productIds.Count)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "Uno o más productos de la lista no existen. No se insertó ningún detalle."
                };
            }

            var prices = products.ToDictionary(p => p.ProductId, p => p.Price);
            var details = request.Details.Select(item =>
            {
                var price = prices[item.IdProduct];
                return new Detail
                {
                    ProductId = item.IdProduct,
                    InvoiceId = request.IdInvoice,
                    Amount = item.Amount,
                    Price = price,
                    SubTotal = item.Amount * price
                };
            }).ToList();

            var existingSubTotal = _context.Details
                .Where(d => d.InvoiceId == request.IdInvoice)
                .Sum(d => d.SubTotal);
            var newSubTotal = details.Sum(d => d.SubTotal);

            invoice.Total = existingSubTotal + newSubTotal;
            _context.Details.AddRange(details);
            _context.SaveChanges();

            return new ResponseV1
            {
                Success = true,
                Message = $"Detalles insertados correctamente. Total de la factura: {invoice.Total}.",
                Id = invoice.InvoiceId
            };
        }
    }
}
