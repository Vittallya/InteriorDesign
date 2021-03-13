using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IUserRegisterService: IDisposable
    {
        event Action Registered;
        string ErrorMessage { get; }
        Task<bool> SetClientPassAndRegister(string pass, string login);
        void SetClientNameAndEmail(string name, string email);
        string Email { get; }
        string Name { get; }
        bool NameAndEmailSetted { get; }
    }

}
