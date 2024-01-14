﻿using Store.Models.Base;
using System.Text.RegularExpressions;

namespace Store.Models
{
    public class Product : BaseModel
    {
        public string Description { get; set; } = null!;
        public int GroupID { get; set; }

        public Group Group { get; set; } = null!;
        public int Price { get; set; }
    }
}