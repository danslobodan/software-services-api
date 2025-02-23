using Application.SoftwareServices.DTOs;
using AutoMapper;
using Domain;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles() {
        CreateMap<PurchaseSoftwareLicenseDto, SoftwareLicense>();
    }
}
