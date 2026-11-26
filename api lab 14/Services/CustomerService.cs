using api_lab_14.Domain;
using api_lab_14.DTO.Request;
using api_lab_14.DTO.Response;
using api_lab_14.Infrastructure;
using api_lab_14.Services.Interfaces;

namespace api_lab_14.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ResponseV1 Insert(CustomerInsertRequestV1 request)
        {
            var documentExists = _context.Customers.Any(c => c.DocumentNumber == request.DocumentNumber);
            if (documentExists)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "Ya existe un cliente con ese número de documento."
                };
            }

            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DocumentNumber = request.DocumentNumber
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return new ResponseV1
            {
                Success = true,
                Message = "Cliente insertado correctamente.",
                Id = customer.CustomerId
            };
        }

        public ResponseV1 Delete(CustomerDeleteRequestV1 request)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == request.Id);
            if (customer == null)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "El cliente no existe."
                };
            }

            var hasInvoices = _context.Invoices.Any(i => i.CustomerId == request.Id);
            if (hasInvoices)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "No se puede eliminar: el cliente tiene facturas asociadas."
                };
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return new ResponseV1
            {
                Success = true,
                Message = "Cliente eliminado correctamente.",
                Id = customer.CustomerId
            };
        }

        public ResponseV1 UpdateDocumentNumber(CustomerUpdateDocumentRequestV1 request)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == request.Id);
            if (customer == null)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "El cliente no existe."
                };
            }

            var documentTaken = _context.Customers.Any(c =>
                c.CustomerId != request.Id && c.DocumentNumber == request.DocumentNumber);
            if (documentTaken)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "Ya existe otro cliente con ese número de documento."
                };
            }

            customer.DocumentNumber = request.DocumentNumber;
            _context.SaveChanges();

            return new ResponseV1
            {
                Success = true,
                Message = "Número de documento actualizado correctamente.",
                Id = customer.CustomerId
            };
        }
    }
}
