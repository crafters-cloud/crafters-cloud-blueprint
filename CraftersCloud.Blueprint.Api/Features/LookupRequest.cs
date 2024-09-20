using MediatR;

namespace CraftersCloud.Blueprint.Api.Features;

public class LookupRequest<T> : IRequest<IEnumerable<LookupResponse<T>>>;