using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using LivetApp1.Models;
using LivetApp1.Services;

namespace LivetApp1.ViewModels
{
    public class LogonViewModel : ViewModel
    {

        #region UserProperty
        private User _User;

        public User User
        {
            get
            { return _User; }
            set
            {
                if (_User == value)
                    return;
                _User = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public void Initialize()
        {
            this.User = new User();
        }

        #region LogonCommand
        private ViewModelCommand _LogonCommand;

        public ViewModelCommand LogonCommand
        {
            get
            {
                if (_LogonCommand == null)
                {
                    _LogonCommand = new ViewModelCommand(Logon);
                }
                return _LogonCommand;
            }
        }

        public async void Logon()
        {
            User authorizedUser = await this.User.LogonAsync();

            if (authorizedUser != null) // Logon 成功
            {
                SessionService session = SessionService.Instance;
                Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Authorized"));
            }
            else // Logon 失敗
            {
                System.Diagnostics.Debug.WriteLine("ログオンに失敗しました。");
            }

        }
        #endregion


    }
}