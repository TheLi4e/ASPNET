using AutoMapper;
using Store.Abstraction;
using Store.Models;
using Store.Models.DTO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Store.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ILogger _logger;


        public ProductRepository(IMapper mapper, IMemoryCache cache, ILogger logger)
        {
            _mapper = mapper;
            _cache = cache;
            _logger = logger;
        }

        public int AddProduct(DTOProduct product)
        {
            using (var context = new StoreContext())
            {
                var entityProduct = context.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (entityProduct is null)
                {
                    entityProduct = _mapper.Map<Product>(product);
                    context.Products.Add(entityProduct);
                    context.SaveChanges();
                    _cache.Remove("products");
                }
                return entityProduct.Id;
            }
        }

        public IEnumerable<DTOProduct> GetProducts()
        {
            using (var context = new StoreContext())
            {
                if (_cache.TryGetValue("products", out List<DTOProduct> products))
                {
                    return products;
                }

                _cache.Set("products", products, TimeSpan.FromMinutes(30));

                products = context.Groups.Select(x => _mapper.Map<DTOProduct>(x)).ToList();

                return products;
            }
        }
        public string GetProductsCSV()
        {
            var sb = new StringBuilder();
            var products = GetProducts();

            foreach (var product in products)
            {
                sb.AppendLine($"{product.Id},{product.Name}, {product.Description}");
            }
            return sb.ToString();
        }

        public string GetСacheStatCSV()
        {
            var curCache = _cache.GetCurrentStatistics();
            var sb = new StringBuilder();
            sb.AppendLine($"CurrentEntryCount, {curCache.CurrentEntryCount.ToString()}")
              .AppendLine($"CurrentEstimatedSize, {curCache.CurrentEstimatedSize.ToString()}")
              .AppendLine($"TotalHits, {curCache.TotalHits.ToString()}")
              .AppendLine($"TotalMisses, {curCache.TotalMisses.ToString()}");
            return sb.ToString();
        }
    }
}