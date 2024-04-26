
namespace InRiseService.Application.Interfaces
{
    public interface ISendGridService
    {
        Task<bool> SendAsync(string toEmail, string name, string subject, string message);
    }
}