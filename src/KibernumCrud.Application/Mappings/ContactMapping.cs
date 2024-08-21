using KibernumCrud.Application.Handlers.V1.Contacts.Queries.GetContact;
using KibernumCrud.Application.Handlers.V1.Contacts.Queries.ListContacts;
using KibernumCrud.Application.Models.V1.Requests.Contacts;
using KibernumCrud.Application.Models.V1.Responses.Contacts;
using KibernumCrud.DataAccess.Entities;
using Riok.Mapperly.Abstractions;

namespace KibernumCrud.Application.Mappings;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class ContactMapping
{
    public static partial ContactDto ToDto(this Contact contact);

    public static partial ListContactsQuery ToQuery(this ListContactsRequest request);
    
    public static partial PaginatedDtoContactsResult ToPaginateResult(this PaginatedContactsResult result);
}