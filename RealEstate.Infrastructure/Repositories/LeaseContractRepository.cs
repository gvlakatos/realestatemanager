using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Repositories;
using RealEstate.Core.Entities;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories;

public class LeaseContractRepository(AppDbContext context) : GenericRepository<LeaseContract>(context), ILeaseContractRepository
{
    public async Task<List<LeaseContract>> GetActiveContractsAsync()
    {
        return await _dbSet.Where(ls => ls.EndDate >= DateTime.UtcNow).ToListAsync();
    }
}