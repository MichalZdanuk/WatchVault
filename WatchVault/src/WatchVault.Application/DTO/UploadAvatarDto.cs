using Microsoft.AspNetCore.Http;

namespace WatchVault.Application.DTO;
public record UploadAvatarDto(IFormFile File);
