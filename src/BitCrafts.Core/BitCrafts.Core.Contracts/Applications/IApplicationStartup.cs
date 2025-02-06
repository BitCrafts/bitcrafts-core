namespace BitCrafts.Core.Contracts.Applications;

public interface IApplicationStartup
{
    void Initialize();
    void Start();
}