using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.MVVM_Core.Events
{
    public class ClientRegistered: IEvent
    {
        public ClientRegistered(IUser user)
        {
            User = user;
        }

        public IUser User { get; set; }

    }
}
