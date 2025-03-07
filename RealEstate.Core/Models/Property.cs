using RealEstate.Core.Enums;

namespace RealEstate.Core.Models;

public class Property
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Address { get; set; } = string.Empty;
    public EPropertyType PropertyType { get; set; }
    public ETransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public EPropertyStatus PropertyStatus { get; set; } = EPropertyStatus.Available;
    public string Description { get; set; } = string.Empty;
    
    public Guid OwnerId { get; set; }
    public Owner Owner { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }

    // Contrato de locação associado (nullable quando o imóvel está disponível)
    public LeaseContract? CurrentContract { get; set; }
}