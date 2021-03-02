using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BL
{
    public interface ILoginService
    {
        Task<bool> TryLogin(string login, string password, bool isAdmin);
        string ErrorMessage { get; }

        void Skip();

    }

    public class LoginService : ILoginService
    {
        private AllDbContext dbContext;
        private readonly ICurrentUserService userService;

        public string ErrorMessage { get; private set; }

        public LoginService(ICurrentUserService userService)
        {
            this.userService = userService;
        }

        public async Task<bool> TryLogin(string login, string password, bool isAdmin)
        {
            await Task.Run(() => dbContext = new AllDbContext());

            await dbContext.Users.LoadAsync();

            Role role = isAdmin ? Role.Admin : Role.User;

            var user = await dbContext.Users.FirstOrDefaultAsync( u =>
                string.Compare(login, u.Email, false) == 0 &&
                string.Compare(password, u.Password, false) == 0 &&
                u.Role == role);

            if(user != null)
            {
                userService.SetupUser(user);
            }
            else 
            {
                ErrorMessage = isAdmin ? "Данные для входа некорректные" : "Пользователь не найден";
            }

            return user != null;
        }

        public void Skip()
        {
            userService.IsSkipped = true;
        }
    }
}
