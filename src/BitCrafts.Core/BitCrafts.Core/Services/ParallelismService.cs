using BitCrafts.Core.Contracts.Services;

namespace BitCrafts.Core.Services;

public sealed class ParallelismService : IParallelismService
{
    public int GetOptimalParallelism(bool isCpuBound = false)
    {
        int processorCount = Environment.ProcessorCount;

        if (isCpuBound)
        {
            return Math.Max(1, processorCount - 1);
        }
        else
        {
            return processorCount * 2;
        }
    }
}