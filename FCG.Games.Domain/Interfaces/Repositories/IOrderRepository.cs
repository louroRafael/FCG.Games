using FCG.Games.Domain.Entities;

namespace FCG.Games.Domain.Interfaces.Repositories;

public interface IOrderRepository : IRepositoryBase<OrderEntity>
{
    Task<List<OrderEntity>> GetByUserIdAsync(Guid userId);
}
