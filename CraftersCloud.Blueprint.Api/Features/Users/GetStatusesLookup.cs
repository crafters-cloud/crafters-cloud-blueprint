﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using CraftersCloud.Blueprint.Domain.Users;
using CraftersCloud.Core.Data;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CraftersCloud.Blueprint.Api.Features.Users;

public static class GetStatusesLookup
{
    [PublicAPI]
    public class Request : LookupRequest<UserStatusId>;

    [UsedImplicitly]
    public class MappingProfile : Profile
    {
        public MappingProfile() =>
            CreateMap<UserStatus, LookupResponse<UserStatusId>>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));
    }

    [UsedImplicitly]
    public class RequestHandler(IMapper mapper, IRepository<UserStatus, UserStatusId> roleRepository)
        : IRequestHandler<Request, IEnumerable<LookupResponse<UserStatusId>>>
    {
        public async Task<IEnumerable<LookupResponse<UserStatusId>>> Handle(Request request,
            CancellationToken cancellationToken) =>
            await roleRepository
                .QueryAll()
                .ProjectTo<LookupResponse<UserStatusId>>(mapper.ConfigurationProvider, cancellationToken)
                .OrderBy(r => r.Label)
                .ToListAsync(cancellationToken);
    }
}