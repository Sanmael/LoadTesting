using System.Diagnostics;

namespace Infra
{
    public class ExecuteRequest
    {
        private int successCount = 0, failureCount = 0, canceledCount = 0;
        private double averageTime = 0;
        private long maxTime = 0;
        private long minTime = 0;
        List<long> requestTimes = new List<long>();

        public RequestMetricsDto ExecuteRequests<T>(Client client, RequestDto requestDto)
        {
            var tasks = AddRequests(client, requestDto);

            Task.WaitAll(tasks.ToArray());

            CalculateTimers();

            return new RequestMetricsDto(requestDto.MethodName, successCount, failureCount, averageTime, maxTime, minTime, tasks.Count, canceledCount, CalculateSuccessRate(tasks.Count));
        }

        private List<Task> AddRequests(Client client, RequestDto requestDto, int maxConcurrency = 6)
        {
            List<Task> tasks = new List<Task>();
            SemaphoreSlim semaphore = new SemaphoreSlim(maxConcurrency);
            object lockObj = new object();

            for (int i = 0; i < requestDto.MaxRequests; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    await semaphore.WaitAsync();

                    try
                    {
                        var stopwatch = Stopwatch.StartNew();

                        var response = await client.SendRequest();

                        stopwatch.Stop();

                        var responseTime = stopwatch.ElapsedMilliseconds;

                        requestTimes.Add(responseTime);

                        lock (lockObj)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                Interlocked.Increment(ref successCount);
                            }
                            else
                            {
                                Interlocked.Increment(ref failureCount);
                            }
                        }
                    }

                    catch (Exception)
                    {
                        Interlocked.Increment(ref canceledCount);
                    }

                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }

            return tasks;

        }

        private void CalculateTimers()
        {
            averageTime = requestTimes.Any() ? requestTimes.Average() : 0;
            maxTime = requestTimes.Any() ? requestTimes.Max() : 0;
            minTime = requestTimes.Any() ? requestTimes.Min() : 0;
        }

        private double CalculateSuccessRate(int totalRequests)
        {
            return (double)successCount / totalRequests * 100;
        }
    }
}
