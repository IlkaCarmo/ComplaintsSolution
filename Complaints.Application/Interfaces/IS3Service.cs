using Microsoft.AspNetCore.Http;

namespace Complaints.Application.Interfaces
{
    public interface IS3Service
    {
        Task UploadFileAsync(IFormFile file, string fileName);
    }
}
