namespace WebAPI.DTO
{
    public record CreateProductDTO
    {      
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int? CategoryId { get; set; }
            public short? UnitsInStock { get; set; }
            public decimal? UnitPrice { get; set; }         
    }
}
