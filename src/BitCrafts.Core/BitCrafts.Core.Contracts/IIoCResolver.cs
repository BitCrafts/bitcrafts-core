namespace BitCrafts.Core.Contracts;

public interface IIoCResolver
{
    TService Resolve<TService>();

    object Resolve(Type type);
    
    TService Resolve<TService>(Type type);
}