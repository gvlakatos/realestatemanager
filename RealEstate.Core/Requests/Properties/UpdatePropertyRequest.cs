using RealEstate.Core.Enums;

namespace RealEstate.Core.Requests.Properties;

public class UpdatePropertyRequest : Request
{
    public string Address { get; set; } = string.Empty;
    public EPropertyType PropertyType { get; set; } = EPropertyType.House;
    public ETransactionType TransactionType { get; set; } = ETransactionType.Rent;
    public decimal Amount { get; set; }
    public EPropertyStatus PropertyStatus { get; set; } = EPropertyStatus.Available;
    public string Description { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
}