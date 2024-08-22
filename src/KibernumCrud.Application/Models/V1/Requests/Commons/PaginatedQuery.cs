namespace KibernumCrud.Application.Models.V1.Requests.Commons;

public abstract record PaginatedQuery()
{
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 20;
    private const char Minus = '-';
    private const char Plus = '+';

    public required int? PageNumber { get; init; } = DefaultPage;

    public required int? PageSize { get; init; } = DefaultPageSize;

    public required string? SortBy { get; init; }

    public string? SortField => SortBy?.Trim(Plus, Minus).Trim();

    public int SafePage => PageNumber ?? DefaultPage;

    public int SafePageSize => PageSize ?? DefaultPageSize;
}