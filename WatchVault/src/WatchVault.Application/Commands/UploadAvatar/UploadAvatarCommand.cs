using Microsoft.AspNetCore.Http;

namespace WatchVault.Application.Commands.UploadAvatar;
public record UploadAvatarCommand(IFormFile File) : ICommand<bool>;
