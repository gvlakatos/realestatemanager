namespace RealEstate.Core.Requests.Tenants;

public class UpdateTenantRequest : Request
{
    public string Name { get; set; } = string.Empty;
    public string CpfCnpj { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}