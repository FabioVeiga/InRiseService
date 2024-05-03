
namespace InRiseService.Application.Interfaces
{
    public interface ISendGridService
    {
        Task<bool> SendAsync(string toEmail, string name, string subject, string message);
        Task<bool> SendByTemplateAsync(string toEmail, string subject, IDictionary<string, string> substitutions, string template);
    }
}