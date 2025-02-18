using BitCrafts.Infrastructure.Abstraction.Threading;

namespace BitCrafts.Infrastructure.Threading;

public sealed class Parallelism : IParallelism
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