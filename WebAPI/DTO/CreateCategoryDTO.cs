namespace WebAPI.DTO
{
    public record CreateCategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
