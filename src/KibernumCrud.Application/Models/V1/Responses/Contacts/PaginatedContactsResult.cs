using KibernumCrud.Application.Models.V1.Responses.Commons;
using KibernumCrud.DataAccess.Entities;

namespace KibernumCrud.Application.Models.V1.Responses.Contacts;

public sealed record PaginatedContactsResult : PaginatedResponse<Contact>
{
    public PaginatedContactsResult(
        int pageNumber,
        int pageSize,
        long totalItems,
        IReadOnlyList<Contact> results)
        : base(pageNumber, pageSize, totalItems, results)
        {
        }
}