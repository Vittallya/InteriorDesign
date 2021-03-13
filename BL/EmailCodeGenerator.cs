using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace BL
{
    public class EmailCodeGenerator : IEmailCodeGenerator
    {
        private byte[] _code;
        private readonly int countOfNumbers;

        public bool CheckCode(string code)
        {
            if(_code.Length != code.Length)
            {
                return false;
            }

            for (int i = 0; i < _code.Length; i++)
            {
                byte sym = _code[i];

                if(!byte.TryParse(code[i].ToString(), out byte symOther) || symOther != sym)
                {
                    return false;
                }

            }
            return true;
        }

        public EmailCodeGenerator(): this(4)
        {

        }
        public EmailCodeGenerator(int countOfNumbers)
        {
            this.countOfNumbers = countOfNumbers;
        }

        public void GenerateCode()
        {
            Random rand = new Random();
            _code = new byte[countOfNumbers];

            for (int i = 0; i < countOfNumbers; i++)
            {
                _code[i] = (byte)rand.Next(0, 10);
            }
        }

        public async Task SendCodeByEmail(string email, IEmailMessageSender messageSender)
        {
            if(_code == null)
            {
                throw new InvalidOperationException("Code was not generated (method 'GenerateCode' was not called)'");
            }

            string send = string.Join(string.Empty, _code.Select(x => x.ToString()));

            //string send = string.Concat(_code.Select(x => Convert.ToChar(x)));
            await messageSender.SendCode(send, email);
        }

        public void Dispose()
        {
            _code = null;
        }
    }
}
