﻿using BL;
using Main.MVVM_Core;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Main.ViewModels
{
    public class ClientCheckCodeViewModel: MVVM_Core.BasePageViewModel
    {
        private readonly IEmailCodeGenerator codeGenerator;
        private readonly IEmailMessageSender messageSender;
        private readonly PageService pageService;
        private readonly IUserRegisterService registerService;



        public ClientCheckCodeViewModel(
            BL.IEmailCodeGenerator codeGenerator, 
            BL.IEmailMessageSender messageSender,
            BL.PageService pageService,
            BL.IUserRegisterService registerService): base(pageService)
        {
            this.codeGenerator = codeGenerator;
            this.messageSender = messageSender;
            this.pageService = pageService;
            this.registerService = registerService;
            Init();
        }


        async void Init()
        {
            await GenetateAndSendCode();
        }

        public string ButtonSendText { get; set; } = "Отправить код повторно";

        public string Code { get; set; }
        public string Message { get; set; }

        public bool IsMessageVisible { get; set; }
        public bool IsNoteVisible { get; set; }

        public bool IsTextBoxEnabled { get; set; }

        public ICommand CloseNote => new Command(x =>
        {
            IsNoteVisible = false;
        });

        async Task GenetateAndSendCode()
        {
            IsMessageVisible = false;
            codeGenerator.GenerateCode();
            try
            {
                await codeGenerator.SendCodeByEmail(registerService.Email, messageSender);
                Counter();
                IsNoteVisible = true;
                IsTextBoxEnabled = true;
            }
            catch (Exception ex)
            {
                IsMessageVisible = true;
                Message = ex.Message;
                SendAgainEnabled = false;
                IsTextBoxEnabled = false;
            }
        }

        public ICommand SendCodeAgain => new MVVM_Core.CommandAsync(async x =>
        {
            await GenetateAndSendCode();
        });

        public ICommand Next => new MVVM_Core.CommandAsync(async x =>
        {
            IsMessageVisible = false;

            try
            {

                if (codeGenerator.CheckCode(Code))
                {
                    pageService.ChangePage<Pages.ClientEnterPasswordPage>(Rules.Pages.CLIENT_REGISTRATION_POOL, AnimateTo.Left);
                }
                else
                {
                    IsMessageVisible = true;
                    Message = "Код неверный";
                }
            }
            catch(Exception ex)
            {
                IsMessageVisible = true;
                Message = ex.Message;
            }
        }, x => Code != null && Code.Length > 0);



        async void Counter()
        {
            SendAgainEnabled = false;
            _sec = messageSender.SendIntervalInSec;
            while (_sec > 0)
            {
                --_sec;
                ButtonSendText = $"Отправить код повторно ({_sec})";
                await Task.Delay(1000);
            }
            ButtonSendText = "Отправить код повторно";
            SendAgainEnabled = true;
        }

        int _sec;
        public bool SendAgainEnabled { get; set; }

        public override int PoolIndex => Rules.Pages.CLIENT_REGISTRATION_POOL;
    }
}
