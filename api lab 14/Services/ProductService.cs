using api_lab_14.Domain;
using api_lab_14.DTO.Request;
using api_lab_14.DTO.Response;
using api_lab_14.Infrastructure;
using api_lab_14.Services.Interfaces;

namespace api_lab_14.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ResponseV1 Insert(ProductInsertRequestV1 request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return new ResponseV1
            {
                Success = true,
                Message = "Producto insertado correctamente.",
                Id = product.ProductId
            };
        }

        public ResponseV1 Delete(ProductDeleteRequestV1 request)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == request.Id);
            if (product == null)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "El producto no existe."
                };
            }

            var hasDetails = _context.Details.Any(d => d.ProductId == request.Id);
            if (hasDetails)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "No se puede eliminar: el producto tiene detalles de factura asociados."
                };
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return new ResponseV1
            {
                Success = true,
                Message = "Producto eliminado correctamente.",
                Id = product.ProductId
            };
        }

        public ResponseV1 UpdatePrice(ProductUpdatePriceRequestV1 request)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == request.Id);
            if (product == null)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "El producto no existe."
                };
            }

            product.Price = request.Price;
            _context.SaveChanges();

            return new ResponseV1
            {
                Success = true,
                Message = "Precio actualizado correctamente.",
                Id = product.ProductId
            };
        }

        public ResponseV1 DeleteList(ProductDeleteListRequestV1 request)
        {
            var ids = request.Products.Select(p => p.Id).Distinct().ToList();
            var products = _context.Products.Where(p => ids.Contains(p.ProductId)).ToList();

            if (products.Count != ids.Count)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = "Uno o más productos de la lista no existen. No se eliminó ninguno."
                };
            }

            var idsWithDetails = _context.Details
                .Where(d => ids.Contains(d.ProductId))
                .Select(d => d.ProductId)
                .Distinct()
                .OrderBy(id => id)
                .ToList();

            if (idsWithDetails.Count > 0)
            {
                return new ResponseV1
                {
                    Success = false,
                    Message = $"Los productos con id {string.Join(", ", idsWithDetails)} tienen detalles de factura asociados. No se eliminó ninguno."
                };
            }

            _context.Products.RemoveRange(products);
            _context.SaveChanges();

            return new ResponseV1
            {
                Success = true,
                Message = $"Se eliminaron {products.Count} productos."
            };
        }
    }
}
