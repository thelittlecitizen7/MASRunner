using System;

namespace MAS.Products
{
    public class ProductBase : IProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ProductBase(string productName, string description)
        {
            Name = productName;
            Description = description;
        }

        public override string ToString()
        {
            string msg = "";
            msg += $"product name : {Name} {Environment.NewLine}";
            msg += $"product description : {Description}";
            return msg;
        }
    }
}
