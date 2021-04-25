using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OpenStore.Omnichannel.Application;

namespace OpenStore.Omnichannel.Infrastructure.ObjectStorage
{
    public class FileSystemObjectStorageService : IObjectStorageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileSystemObjectStorageService(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<(string host, string path)> Write(string fileName, byte[] content)
        {
            const string rootDir = "content";
            var dir = Path.Combine(_webHostEnvironment.WebRootPath, rootDir);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            await using var fs = System.IO.File.Create(Path.Combine(dir, fileName));
            await fs.WriteAsync(content.AsMemory(0, content.Length));

            Debug.Assert(_httpContextAccessor.HttpContext != null, "_httpContextAccessor.HttpContext != null");
            return ($"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}", $"/{rootDir}/{fileName}");
        }
    }
}