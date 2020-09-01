namespace MAS.Products
{
    public interface IProduct
    {
        public string Name { get; set; }

        public string Description { get; set; }

        string ToString();
    }
}
