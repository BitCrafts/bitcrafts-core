namespace BitCrafts.Infrastructure.Abstraction.Threading;

public interface IParallelism
{
    int GetOptimalParallelism(bool isCpuBound = false);
}