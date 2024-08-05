using BackEndMCF.ViewModels;

namespace BackEndMCF.Interface
{
    public interface IAccountServices
    {
        Task<(bool status, string message, ResLogin data)> LoginAccount(ReqLogin req);
    }
}
