using FCG.Games.Domain.Entities;
using FCG.Games.Domain.Interfaces.Repositories;
using FCG.Games.Infra.Contexts;

namespace FCG.Games.Infra.Repository;

public class PromotionRepository : RepositoryBase<PromotionEntity>, IPromotionRepository
{
    public PromotionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
