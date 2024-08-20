namespace KibernumCrud.Application.Models.V1.Responses.Commons;

public abstract record PaginatedResponse<T>
{
    protected PaginatedResponse(int pageNumber, int pageSize, long totalItems, IReadOnlyList<T> results)
    {
        PageNumber = Math.Max(pageNumber, 1);
        PageSize = Math.Max(pageSize, 1);
        TotalItems = Math.Max(totalItems, 0);
        Results = results;
    }
    
    public int TotalPages => PageSize > 0 ? (int)((TotalItems + PageSize - 1) / PageSize) : 0;

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 20;

    public long TotalItems { get; init; }

    public IReadOnlyList<T> Results { get; init; }

    public bool HasNextPage => PageNumber < TotalPages;

    public bool HasPreviousPage => PageNumber > 1;
}