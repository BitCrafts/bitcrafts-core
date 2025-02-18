namespace BitCrafts.Infrastructure.Abstraction.Repositories;

public class RepositorySearchParameter
{
    public RepositorySearchParameter()
    {
        Page = 1;
        PageSize = 10;
        OrderBy = "Id";
        Descending = true;
    }

    public string Keyword { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string OrderBy { get; set; }
    public bool Descending { get; set; }

    public List<RepositoryFilterCondition> Conditions { get; set; } = new();

    public void Validate()
    {
        if (Page < 1) throw new ArgumentOutOfRangeException(nameof(Page), "La page doit être supérieure ou égale à 1.");

        if (PageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(PageSize),
                "La taille de la page doit être supérieure ou égale à 1.");
    }
}