using FCG.Games.Domain.Enums;
using FCG.Games.Domain.Interfaces.Repositories;

namespace FCG.Games.Domain.Entities;

public class GameEntity : EntityBase, IAggregateRoot
{
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string Genre { get; private set; } = default!;
    public string Platform { get; set; } = default!;
    public string Developer { get; set; } = default!;
    public decimal Price { get; private set; }

    public GameEntity() { }

    public GameEntity(string title, string description, GameGenre genre, GamePlatform platform, string developer, decimal price)
    {
        Title = title.Trim();
        Description = description.Trim();
        Genre = genre.ToString();
        Platform = platform.ToString();
        Developer = developer;
        Price = price;
    }

    public void UpdatePrice(decimal newPrice)
    {
        Price = newPrice;
    }
}
