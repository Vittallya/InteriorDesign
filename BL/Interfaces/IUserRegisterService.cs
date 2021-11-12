using DAL.Models;
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
        Task<bool> TryRegister(string pass, string login);

        Client GetRegistered();

        void SetClientNameAndEmail(string name, string email);
        string Email { get; }
        string Name { get; }
        bool NameAndEmailSetted { get; set; }
    }

}
