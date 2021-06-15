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

        public bool IsAdmin { get; private set; }

        public event Action Autorized;
        public event Action Exited;
        public event Action Skipped;

        public void Skip()
        {
            IsSkipped = true;
            IsAutorized = false;
            Skipped?.Invoke();
        }

        public void Logout()
        {
            CurrentUser = null;
            IsAutorized = IsSkipped = false;
            Exited?.Invoke();
        }

        public void SetupUser(IUser user, bool isAdmin = false)
        {
            CurrentUser = user;
            IsAutorized = true;
            IsAdmin = isAdmin;
            Autorized?.Invoke();
        }
    }
}
