using AutoMapper;
using Domain;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles() {
        CreateMap<Account, Account>();
        CreateMap<Customer, Customer>();
        CreateMap<SoftwareService, SoftwareService>();
    }
}
