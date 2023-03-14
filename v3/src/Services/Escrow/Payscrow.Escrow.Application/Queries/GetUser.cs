using AutoMapper;
using MediatR;
using Payscrow.Escrow.Application.Common.Mappings;
using Payscrow.Escrow.Application.Services;
using Payscrow.Escrow.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.Queries
{
    public static class GetUser
    {
        public class Query : BaseQuery<QueryResult>
        {
            public Guid? UserGuid { get; set; }

            private class Handler : IRequestHandler<Query, QueryResult>
            {
                private readonly UserService _userService;
                private readonly IMapper _mapper;

                public Handler(UserService userService, IMapper mapper)
                {
                    _userService = userService;
                    _mapper = mapper;
                }

                public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
                {
                    if (!request.UserGuid.HasValue || request.UserGuid == Guid.Empty) return null;

                    var user = await _userService.GetUserAsync(request.UserGuid.Value, request.TenantId);

                    return _mapper.Map<QueryResult>(user);
                }
            }
        }

        public class QueryResult : IMapFrom<User>
        {
            public Guid Id { get; set; }
            public Guid UserGuid { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string AvatarFileName { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<User, QueryResult>();
            }
        }
    }
}