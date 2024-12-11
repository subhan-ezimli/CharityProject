using A.Domain.Entities;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace D.Dal.SqlServer.Infrastructure;

public class SqlPaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _dbContext;
    public SqlPaymentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateFinishedPaymentAsync(Payment payment)
    {
        await _dbContext.Payments.AddAsync(payment);
    }

    public async Task CreatePendingPaymentAsync(PendingPayment pendingPayment)
    {
        await _dbContext.PendingPayments.AddAsync(pendingPayment);
    }

    public async Task<PendingPayment?> GetPendingPaymentByDescriminatorAsync(string orderId)
    {
        return await _dbContext.PendingPayments
            .FirstOrDefaultAsync(i => i.Discriminator == orderId);
    }
}
