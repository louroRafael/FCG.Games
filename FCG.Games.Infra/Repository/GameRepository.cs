using FCG.Games.Domain.Entities;
using FCG.Games.Domain.Interfaces.Repositories;
using FCG.Games.Infra.Contexts;

namespace FCG.Games.Infra.Repository;

public class GameRepository : RepositoryBase<GameEntity>, IGameRepository
{
    public GameRepository(ApplicationDbContext context) : base(context)
    {
    }
}
