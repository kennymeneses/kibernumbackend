using KibernumCrud.Application.Models.V1.Requests.Commons;

namespace KibernumCrud.Application.Models.V1.Requests.Contacts;

public sealed record ListContactsRequest(Guid UserId) : PaginatedRequest;