using FCG.Games.Domain.Entities;
using FCG.Games.Domain.Interfaces.Repositories;
using FCG.Games.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FCG.Games.Infra.Repository;

public class OrderRepository : RepositoryBase<OrderEntity>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<OrderEntity>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet.AsNoTracking()
            .Include(o => o.Items)
                .ThenInclude(oi => oi.Game)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<OrderEntity>> GetAllWithItems(CancellationToken ct = default)
    {
        return await _dbSet
                .AsNoTracking()
                .Include(o => o.Items)
                .ToListAsync(ct);
    }
}