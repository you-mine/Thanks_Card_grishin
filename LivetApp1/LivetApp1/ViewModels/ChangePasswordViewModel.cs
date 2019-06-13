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
using System.Windows;

namespace LivetApp1.ViewModels
{
    public class ChangePasswordViewModel : ViewModel
    {
        private IEditUserService service = new EditUserPutService();

        #region OldPassword

        private string _OldPassword;

        public string OldPassword
        {
            get
            { return _OldPassword; }
            set
            { 
                if (_OldPassword == value)
                    return;
                _OldPassword = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region NewPassword
        private string _NewPassword;

        public string NewPassword
        {
            get
            { return _NewPassword; }
            set
            {
                if (_NewPassword == value)
                    return;
                _NewPassword = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region User

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

        #region ChangeCommand

        private ViewModelCommand _ChangeCommand;

        public ViewModelCommand ChangeCommand
        {
            get
            {
                if (_ChangeCommand == null)
                {
                    _ChangeCommand = new ViewModelCommand(Change);
                }
                return _ChangeCommand;
            }
        }

        public async void Change()
        {

            string inputcheck = InputCheck();

            if (string.IsNullOrEmpty(inputcheck))
            {
                User.Password = NewPassword;
                string result = await service.EditUserAsync(this.User);
                if (result == "success")
                {
                    MessageBox.Show("パスワードの変更に成功しました。");
                    Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
                }
                else
                {
                    MessageBox.Show("パスワードの変更に失敗しました。");
                }
            }
            else
            {
                MessageBox.Show(inputcheck);
            }
        }

        #endregion

        #region CloseCommand

        private ViewModelCommand _CloseCommand;

        public ViewModelCommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = new ViewModelCommand(Close);
                }
                return _CloseCommand;
            }
        }

        public void Close()
        {
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
        }

        #endregion



        private string InputCheck()
        {
            string error = "";

            if (string.IsNullOrEmpty(OldPassword))
            {
                error += "現在のパスワードを入力してください。\n";
            }
            else if(User.Password != OldPassword)
            {
                error += "入力された現在のパスワードが間違っています。\n";
            }

            if (string.IsNullOrEmpty(NewPassword))
            {
                error += "新しいパスワードを入力してください。\n";
            }
            return error;
        }


        public void Initialize()
        {
            this.User = SessionService.Instance.AuthorizedUser;
        }


    }
}
