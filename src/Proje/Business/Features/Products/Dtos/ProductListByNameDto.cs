namespace Business.Features.Products.Dtos
{
    public class ProductListByNameDto
    {
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
