using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Core.Data;
using JetBrains.Annotations;
using MediatR;

namespace CraftersCloud.Blueprint.Api.Features.Users;

public static class GetCompanyLookup
{
    [PublicAPI]
    public class Request : LookupRequest<Guid>;

    [UsedImplicitly]
    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<Company, LookupResponse<Guid>>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));
    }
}
