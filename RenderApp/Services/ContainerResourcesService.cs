using System.Diagnostics;

namespace RenderApp.Services
{
    public class ContainerResourcesService
    {
        // --- Лимиты памяти ---
        public string GetMemoryLimit()
        {
            try
            {
                var path = "/sys/fs/cgroup/memory/memory.limit_in_bytes";
                if (File.Exists(path))
                {
                    var raw = File.ReadAllText(path).Trim();
                    var limit = long.Parse(raw);
                    if (limit > 9_000_000_000_000_000_000)
                        return "Без лимита (вся память хоста)";
                    return $"{limit / (1024 * 1024)} MB";
                }
            }
            catch { }
            return "Не удалось определить";
        }

        // --- Текущее использование памяти ---
        public string GetMemoryUsage()
        {
            using var proc = Process.GetCurrentProcess();
            return $"{proc.WorkingSet64 / (1024 * 1024)} MB";
        }

        // --- Лимиты CPU ---
        public string GetCpuLimit()
        {
            try
            {
                var quotaPath = "/sys/fs/cgroup/cpu/cpu.cfs_quota_us";
                var periodPath = "/sys/fs/cgroup/cpu/cpu.cfs_period_us";

                if (File.Exists(quotaPath) && File.Exists(periodPath))
                {
                    var quota = int.Parse(File.ReadAllText(quotaPath).Trim());
                    var period = int.Parse(File.ReadAllText(periodPath).Trim());

                    if (quota > 0)
                    {
                        var cpuCount = (double)quota / period;
                        return $"{cpuCount:F2} vCPU";
                    }
                    return "Без лимита (все CPU хоста)";
                }
            }
            catch { }
            return "Не удалось определить";
        }

        // --- Текущее использование CPU ---
        public string GetCpuUsage()
        {
            using var proc = Process.GetCurrentProcess();
            return $"{proc.TotalProcessorTime.TotalSeconds:F2} сек CPU времени";
        }

        // --- Диск (DriveInfo показывает доступное пространство) ---
        public IEnumerable<(string Drive, long Total, long Free)> GetDiskInfo()
        {
            return DriveInfo.GetDrives()
                .Where(d => d.IsReady)
                .Select(d => (d.Name,
                              d.TotalSize / (1024 * 1024 * 1024),
                              d.AvailableFreeSpace / (1024 * 1024 * 1024)));
        }
    }
}
