using A.Domain.Entities;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace D.Dal.SqlServer.Infrastructure;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;
    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateFinishedPaymentAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
    }

    public async Task CreatePendingPaymentAsync(PendingPayment pendingPayment)
    {
        await _context.PendingPayments.AddAsync(pendingPayment);
    }

    public async Task<PendingPayment?> GetPendingPaymentByDescriminatorAsync(string orderId)
    {
        return await _context.PendingPayments
           .FirstOrDefaultAsync(i => i.Discriminator == orderId);
    }
}
