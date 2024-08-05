using BackEndMCF.ViewModels;

namespace BackEndMCF.Interface
{
    public interface ITransactionServices
    {
        Task<(bool status, string message)> AddDataBPKB(ReqAddBPKB req);
        Task<(bool status, string message, List<ResDropdown> data)> DropdownStorage();
    }
}
