namespace FCG.Games.Domain.DTOs.ElasticDocuments;

public class GameElasticDocument
{
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Genre { get; set; } = default!;
    public string Platform { get; set; } = default!;
    public string Developer { get; set; } = default!;
    public decimal Price { get; set; }
    public DateTime CreateDate { get; set; }
}
