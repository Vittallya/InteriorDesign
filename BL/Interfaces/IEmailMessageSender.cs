using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IEmailMessageSender
    {
        string ErrorMessage { get; }
        int SendIntervalInSec { get; }
        Task<bool> SendCode(string code, string emailReciever);
    }
}
