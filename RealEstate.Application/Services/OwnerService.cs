using RealEstate.Application.Interfaces;
using RealEstate.Application.Repositories;
using RealEstate.Core.Entities;
using RealEstate.Core.Requests.Owners;

namespace RealEstate.Application.Services;

public class OwnerService(IOwnerRepository ownerRepository) : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository = ownerRepository;

    public async Task<Owner> CreateAsync(CreateOwnerRequest request)
    {
        var owner = new Owner
        {
            Name = request.Name,
            CpfCnpj = request.CpfCnpj,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            UpdatedAt = DateTime.UtcNow
        };

        return await _ownerRepository.CreateAsync(owner);
    }

    public async Task<Owner> UpdateAsync(UpdateOwnerRequest request)
    {
        var owner = await _ownerRepository.GetByIdAsync(request.Id);
        
        owner.Name = request.Name;
        owner.CpfCnpj = request.CpfCnpj;
        owner.PhoneNumber = request.PhoneNumber;
        owner.Email = request.Email;
        owner.UpdatedAt = DateTime.UtcNow;

        return await _ownerRepository.UpdateAsync(owner);
    }

    public async Task<Owner> GetByIdAsync(GetOwnerByIdRequest request)
    {
        return await _ownerRepository.GetByIdAsync(request.id);
    }
}