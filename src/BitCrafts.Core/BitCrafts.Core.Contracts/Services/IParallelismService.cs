namespace BitCrafts.Core.Contracts.Services;

public interface IParallelismService
{
    int GetOptimalParallelism(bool isCpuBound = false);
}