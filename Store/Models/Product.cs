﻿
namespace Store.Models
{
    public class Product : BaseModel
    {
        public string Description { get; set; } = null!;
        public int GroupID { get; set; }
        public virtual Group Group { get; set; } = null!;
        public int Price { get; set; }
        public virtual List<Warehouse> Stores { get; set; } = null!;
    }
}