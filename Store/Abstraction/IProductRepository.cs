using Store.Models.DTO;

namespace Store.Abstraction
{
    public interface IProductRepository
    {
        public int AddProduct(DTOProduct product);
        public string GetProductsCSV();
        public string GetСacheStatCSV();

        public IEnumerable<DTOProduct> GetProducts();

    }
}
