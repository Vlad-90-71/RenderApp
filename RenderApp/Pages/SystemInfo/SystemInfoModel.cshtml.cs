using Microsoft.AspNetCore.Mvc.RazorPages;
using RenderApp.Services;

namespace RenderApp.Pages.SystemInfo
{
    public class SystemInfoModel : PageModel
    {
        private readonly ContainerResourcesService _resources = new();

        public string? CpuLimit { get; set; }
        public string? CpuUsage { get; set; }
        public string? MemoryLimit { get; set; }
        public string? MemoryUsage { get; set; }
        public IEnumerable<(string Drive, long Total, long Free)> Disks { get; set; } = [];

        public void OnGet()
        {
            CpuLimit = _resources.GetCpuLimit();
            CpuUsage = _resources.GetCpuUsage();
            MemoryLimit = _resources.GetMemoryLimit();
            MemoryUsage = _resources.GetMemoryUsage();
            Disks = _resources.GetDiskInfo();
        }
    }

}
