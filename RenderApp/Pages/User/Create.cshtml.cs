using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RenderApp.Pages.User
{
    public class CreateModel(RenderAppDbContext context) : PageModel
    {
        private readonly RenderAppDbContext _context = context;
        private readonly string _dropboxToken = "sl.u.AGT0j70EsDZBd6oM6I0NL97uN-5TMvsMlEBgQK6oid7vlRup6mFLQQdnJp3ZeKtozbqcySKGxYRWznr-hmvK8K4LZJsvABD9JDAkdim8R1UVX5Td1ZebXYptg2KtRR3UvpFAhgCPuhgKDlIfpGRjsBtcFvSqr-3PZlDKUZFfvhfunnEZG1khtMQs2e6IojiEDxzrinifu11yaTn2Z-SG3n0P33snm0p6HK8-NcSF4DsbJCtOX_zcxHrdxdNr7nouorXcgcm5vA11VfzfQ4o8qCz1HTOnXeS-9fnXme1U9mbsuQuQ4pbZz0f91J2NuAXB3bc2xzQ5ny-Gj-VfSz-MKmkNiWg4cIoyvWQCDh3_KrrEf03g4KznQBRza6UoZ2v5eeyxiZNp1R4aZN_SvzFTTmVL5nzQeJKeAY0_FETW1s9DE1QuDQ6gEeQB1ouvFLPnwgh6qlJ3aJsaZ8s9MIgrSYOfwn1AXsqURkURqNohhHDjFdTeGEMQVoWoSgxBNbe1xPCKE5QBmEFO1qk2ZWEcrGF6QiN--gaFOdlxHfPZvYh15pNILPwDZeES61UWmjcFG9XvkZ-5hVA-45CYrZKZExS0HwRK0cSTvqJG5ihocw89Fl6a9ItL4HalVYzDlLgGa_2c2OlHIH7wA4MoMm9bG00RHPpgSgHqGj3lBXGnB_s1uPkcp2z9ygZn_Xufm6NDEzGQUMj6akkBvgVFRvJJq_Rgccpe3ipY59MbGD1PtyiIXkEfJPF2WLCVS1JmigA8srk3qgV3YpXTnC37i9JlNTQMBsszAqYpV2jOxDmsuNm3q4uudfr_7QSB_wOciLgcSoXpJXJahLEFDuPpzdtS2-lnoBgUCGLvXA1ZXnfY1PexqHgmqQ7vXNUA-R7VLW5WCOxToXi6mQB6MesuJdtWoUZtFZ1p4ZCT08hWHi6P-8Jp7ue6S4hClls7shZ2d8lBarNR_VyfUO0bDujNe88LjRQP2jYVUrahXl66GeGNib1uNFI9zDynPa8hOG6wtqTqWgTWj9bGh7autaf-ea4e3TPgUfp7KlCDX4N29CVtkoK_CBodf9__wvWHPwwUD_ueMW_f4coPZBH1x1Th-tF-EXdTAY7NA3uqvaGSeBcZ4kVK8vyTWbiTdMmlg3_pExjNumPei5Kz07SvSxwZ1t1PphDYbbJ7nFZ3mtORiTD-sqsd3XgNa4GoVXIF71uyMn9nrw_JWGwoWw8dgK6Jy6cx99k8om6LBKBbPspMGGH6GP-IBA";

        [BindProperty]
        public Entity.User NewUser { get; set; } = new();
        
        [BindProperty] 
        public IFormFile? UploadPhoto { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (UploadPhoto != null)
            {
                using var dbx = new DropboxClient(_dropboxToken);
                var dropboxPath = $"/photos/{UploadPhoto.FileName}";

                using var stream = UploadPhoto.OpenReadStream();
                var uploaded = await dbx.Files.UploadAsync(dropboxPath, WriteMode.Overwrite.Instance, body: stream);

                var link = await dbx.Sharing.CreateSharedLinkWithSettingsAsync("/photos/" + UploadPhoto.FileName);
                var directUrl = link.Url.Replace("&dl=0", "&raw=1");
                NewUser.PhotoPath = directUrl; // сохраняем URL в базе
            }

            _context.Users.Add(NewUser);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}

