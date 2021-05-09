using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenStore.Omnichannel.Application;

namespace OpenStore.Omnichannel.Infrastructure.ObjectStorage
{
    public class FileSystemObjectStorageService : IObjectStorageService
    {
        private const string RootDir = "content";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FileSystemObjectStorageService> _logger;

        public FileSystemObjectStorageService(
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment,
            ILogger<FileSystemObjectStorageService> logger
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<(string host, string path)> Write(string fileName, byte[] content)
        {
            var dir = Path.Combine(_webHostEnvironment.WebRootPath, RootDir);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            await using var fs = File.Create(Path.Combine(dir, fileName));
            await fs.WriteAsync(content.AsMemory(0, content.Length));

            Debug.Assert(_httpContextAccessor.HttpContext != null, "_httpContextAccessor.HttpContext != null");
            return ($"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}", $"/{RootDir}/{fileName}");
        }

        public Task Delete(string host, string path)
        {
            var filename = Path.Combine(_webHostEnvironment.WebRootPath, path.Trim('/'));

            return Task.Run(() =>
            {
                try
                {
                    File.Delete(filename);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"File could not be deleted {filename}");
                    throw;
                }
            });
        }
    }
}