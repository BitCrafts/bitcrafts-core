namespace BitCrafts.Infrastructure.Abstraction.Databases;

public interface ISqlDialectFactory
{
    ISqlDialect Create();
}