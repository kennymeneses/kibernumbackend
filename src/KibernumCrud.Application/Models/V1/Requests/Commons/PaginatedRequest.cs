namespace KibernumCrud.Application.Models.V1.Requests.Commons;

public abstract record PaginatedRequest
{
    public const int DefaultPage = 1;
    public const int DefaultPageSize = 20;
    private const char Minus = '-';
    private const char Plus = '+';

    public int? PageNumber { get; set; } = DefaultPage;

    public int? PageSize { get; set; } = DefaultPageSize;

    public string? SortBy { get; set; }

    protected string? SortField => SortBy?.Trim(Plus, Minus).Trim();

    protected int SafePage => PageNumber ?? DefaultPage;

    protected int SafePageSize => PageSize ?? DefaultPageSize;
}