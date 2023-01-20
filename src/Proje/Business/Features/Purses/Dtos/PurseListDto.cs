namespace Business.Features.Purses.Dtos
{
    public class PurseListDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Money { get; set; }
    }
}
