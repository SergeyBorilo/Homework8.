using System.Diagnostics;

namespace Homework8;

class Program
{
    private static async Task Main(string[] args)
    {
        var count = 100;

        var syncStopwatch = Stopwatch.StartNew();
        SyncMethod(count);
        syncStopwatch.Stop();
        Console.WriteLine($"{count} sync in {syncStopwatch.ElapsedMilliseconds} ms");

        var asyncStopwatch = Stopwatch.StartNew();
        await AsyncMethod(count);
        asyncStopwatch.Stop();
        Console.WriteLine($"{count} async in {asyncStopwatch.ElapsedMilliseconds} ms");

        void SyncMethod(int count)
        {
            for (var i = 0; i < count; i++)
            {
                try
                {
                    var fileName = $"file_{i}.txt";
                    File.WriteAllText(fileName, i.ToString());
                    File.Delete(fileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
        }

        async Task AsyncMethod(int count)
        {
            for (var i = 0; i < count; i++)
            {
                try
                {
                    await Task.Run(async () =>
                    {
                        var fileName = $"file_{i}.txt";
                        await File.WriteAllTextAsync(fileName, i.ToString());
                        File.Delete(fileName);
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }
    }
}
