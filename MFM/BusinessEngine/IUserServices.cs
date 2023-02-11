using MFM.Models;

namespace MFM.BusinessEngine
{
    public interface IUserServices
    {
       ApplicationUser getCurrentUser();
        string getCurrentUserID();
    }
}
