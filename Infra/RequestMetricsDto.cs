namespace Infra
{
    public class RequestMetricsDto
    {
        public string MethodName { get; }
        public int SuccessCount { get; }
        public int FailureCount { get; }
        public double AverageTime { get;}
        public long MaxTime { get; }
        public long MinTime { get; }
        public long RequesTimes { get; }
        public long CanceledCount { get; }
        public double SuccessRate { get; set; }
        public RequestMetricsDto(string methodName, int successCount, int failureCount, double averageTime, long maxTime, long minTime, long requesTimes, long canceledCount, double successRate)
        {
            MethodName = methodName;
            SuccessCount = successCount;
            FailureCount = failureCount;
            AverageTime = averageTime;
            MaxTime = maxTime;
            MinTime = minTime;
            RequesTimes = requesTimes;
            CanceledCount = canceledCount;
            SuccessRate = successRate;
        }
    }
}