namespace Business.Features.Products.Dtos
{
    public class CreatedProductDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
