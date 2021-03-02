using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;


namespace BL
{
    public interface ICurrentUserService
    {
        User CurrentUser { get; }
        void Logout();

        bool IsSkipped { get; set; }
        bool IsAutorized { get; }

        event Action Autorized;
        event Action Exited;

        void SetupUser(User user);

    }

    public class UserService : ICurrentUserService
    {
        public User CurrentUser { get; private set; }



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

        public void SetupUser(User user)
        {
            CurrentUser = user;
            IsAutorized = true;
            Autorized?.Invoke();
        }
    }
}
