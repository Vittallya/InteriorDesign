using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface ILoginService
    {
        Task<bool> TryLogin(string login, string password, bool isAdmin);
        string ErrorMessage { get; }
        void Skip();
    }
}
