using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserRegisterService : IUserRegisterService
    {
        private readonly AllDbContext allDbContext;
        private readonly ICurrentUserService userService;

        public event Action Registered;
        public int DelayIntervalSec { get; } = 3;

        public string ErrorMessage { get; private set; }

        public UserRegisterService( AllDbContext allDbContext, ICurrentUserService userService)
        {
            this.allDbContext = allDbContext;
            this.userService = userService;

        }

        string _name;
        public string Email { get; private set; }
        public string Name => _name;

        public bool NameAndEmailSetted { get; set; }

        public void SetClientNameAndEmail(string name, string email)
        {
            _name = name;
            Email = email;
            NameAndEmailSetted = true;
        }

        public async Task<bool> SetClientPassAndRegister(string pass, string login)
        {
            await allDbContext.Clients.LoadAsync();

            if(await allDbContext.Clients.FirstOrDefaultAsync(x => x.Password == pass) != null)
            {
                ErrorMessage = "Такой пароль уже есть";
                return false;
            }

            Client client = new Client { Login = login, Email = Email, Name = _name, Password = pass };
            allDbContext.Clients.Add(client);

            try
            {
                await allDbContext.SaveChangesAsync();
                userService.SetupUser(client);
                Registered?.Invoke();
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
            return true;
        }

        

        public void Dispose()
        {

            //codeGenerator.Dispose();
        }
    }
}
