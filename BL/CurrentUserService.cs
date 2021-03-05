using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;


namespace BL
{
    public class UserService : ICurrentUserService
    {
        public IUser CurrentUser { get; private set; }



        public bool IsAutorized { get; private set; }

        public bool IsSkipped { get; set; }

        public event Action Autorized;
        public event Action Exited;

        public void Logout()
        {
            CurrentUser = null;
            IsAutorized = false;
            Exited?.Invoke();
        }

        public void SetupUser(IUser user)
        {
            CurrentUser = user;
            IsAutorized = true;
            Autorized?.Invoke();
        }
    }
}
