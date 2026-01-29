using Microsoft.AspNetCore.Mvc.RazorPages;
using RenderApp.Services;

namespace RenderApp.Pages.SystemInfo
{
    public class SystemInfoModel : PageModel
    {
        private readonly ContainerMetricsService _metrics;

        public string? CpuUsage { get; set; }
        public string? MemoryUsage { get; set; }
        public IEnumerable<(string Drive, long Total, long Free)> Disks { get; set; } = [];
        public string? ProcMemInfo { get; set; }

        public SystemInfoModel()
        {
            _metrics = new ContainerMetricsService();
        }

        public void OnGet()
        {
            CpuUsage = _metrics.GetCpuUsage();
            MemoryUsage = _metrics.GetMemoryUsage();
            Disks = _metrics.GetDiskInfo();
            ProcMemInfo = _metrics.GetProcMemInfo();
        }
    }

}
