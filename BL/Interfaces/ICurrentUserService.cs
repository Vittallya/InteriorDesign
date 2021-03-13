using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BL
{
    public interface ICurrentUserService
    {
        IUser CurrentUser { get; }
        void Logout();

        bool IsSkipped { get; set; }
        bool IsAutorized { get; }

        event Action Autorized;
        event Action Exited;
        event Action Skipped;
        void Skip();
        void SetupUser(IUser user);

    }
}
