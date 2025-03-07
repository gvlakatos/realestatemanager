using RealEstate.Core.Enums;

namespace RealEstate.Core.Models;

public class LeaseContract
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PropertyId { get; set; }
    public Property Property { get; set; } = null!;

    public Guid OwnerId { get; set; }
    public Owner Owner { get; set; } = null!;

    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal MonthlyAmount { get; set; }
    public EPaymentMethod PaymentMethod { get; set; }
    public ELeaseContractStatus Status { get; set; } = ELeaseContractStatus.Active;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}