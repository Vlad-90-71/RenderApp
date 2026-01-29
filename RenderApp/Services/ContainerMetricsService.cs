using System.Diagnostics;

namespace RenderApp.Services
{
    public class ContainerMetricsService
    {
        public string GetCpuUsage()
        {
            using var proc = Process.GetCurrentProcess();
            return $"{proc.TotalProcessorTime.TotalSeconds:F2} сек CPU времени";
        }

        public string GetMemoryUsage()
        {
            using var proc = Process.GetCurrentProcess();
            return $"{proc.WorkingSet64 / (1024 * 1024)} MB";
        }

        public IEnumerable<(string Drive, long Total, long Free)> GetDiskInfo()
        {
            return DriveInfo.GetDrives()
                .Where(d => d.IsReady)
                .Select(d => (d.Name,
                              d.TotalSize / (1024 * 1024 * 1024),
                              d.AvailableFreeSpace / (1024 * 1024 * 1024)));
        }

        public string GetProcMemInfo()
        {
            var lines = File.ReadAllLines("/proc/meminfo");
            return string.Join("\n", lines.Take(3)); // первые строки: MemTotal, MemFree, Buffers
        }
    }
}
