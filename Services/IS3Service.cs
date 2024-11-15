using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LaundryAPI.Services
{
    public interface IS3Service
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<byte[]> DownloadFileAsync(string fileKey);
    }
}
