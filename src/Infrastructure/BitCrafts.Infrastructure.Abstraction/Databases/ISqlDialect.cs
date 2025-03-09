namespace BitCrafts.Infrastructure.Abstraction.Databases;

public interface ISqlDialect
{
    string GetLastInsertedIdQuery();
}