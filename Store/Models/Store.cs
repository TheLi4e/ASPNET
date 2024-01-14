
namespace Store.Models
{
    public class Store : BaseModel
    {
        public int Count { get; set; }
        public virtual List<Product> Products { get; set; } = null!;
    }
}