using RealEstate.Core.Enums;

namespace RealEstate.Core.Requests.LeaseContracts;

public class CreateLeaseContractRequest
{
    public Guid PropertyId { get; set; }

    public Guid OwnerId { get; set; }

    public Guid TenantId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal MonthlyAmount { get; set; }
    public EPaymentMethod PaymentMethod { get; set; }
}