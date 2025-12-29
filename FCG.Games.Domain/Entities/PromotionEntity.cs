namespace FCG.Games.Domain.Entities;

public class PromotionEntity : EntityBase
{
    public string Title { get; set; }
    public int PercentualDiscount { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    
    public Guid GameId { get; private set; }
    public virtual GameEntity? Game { get; private set; }

    public PromotionEntity() { }

    public PromotionEntity(string title, int discount, DateTime start, DateTime end, Guid gameId)
    {
        Title = title.Trim();
        PercentualDiscount = discount;
        StartDate = start; 
        EndDate = end;
        GameId = gameId;
    }
}
