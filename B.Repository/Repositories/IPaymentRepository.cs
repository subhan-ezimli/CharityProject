using A.Domain.Entities;

namespace B.Repository.Repositories;

public interface IPaymentRepository
{
    Task CreatePendingPaymentAsync(PendingPayment pendingPayment);
    Task<PendingPayment?> GetPendingPaymentByDescriminatorAsync(string orderId);
    Task CreateFinishedPaymentAsync(Payment payment);
}
