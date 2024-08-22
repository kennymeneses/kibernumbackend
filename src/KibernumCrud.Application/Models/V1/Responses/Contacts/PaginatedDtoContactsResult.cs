using KibernumCrud.Application.Models.V1.Responses.Commons;

namespace KibernumCrud.Application.Models.V1.Responses.Contacts;

public sealed record PaginatedDtoContactsResult: PaginatedResponse<ContactDto>
{
    public PaginatedDtoContactsResult(
    int pageNumber,
    int pageSize,
    long totalItems,
    IReadOnlyList<ContactDto> results) : base(pageNumber, pageSize, totalItems, results)
    {
    }
}