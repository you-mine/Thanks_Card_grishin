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
    public class EditUserViewModel : ViewModel
    {

        IEditUserService service = null;

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

        #region DepartmentProperrty
        private List<Department> _Departments;

        public List<Department> Departments
        {
            get
            { return _Departments; }
            set
            { 
                if (_Departments == value)
                    return;
                _Departments = value;
                RaisePropertyChanged();
            }
        }

        #endregion

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

        #region SelectDepartment Command
        private ListenerCommand<Department> _SelectDepartmentCommand;


        public ListenerCommand<Department> SelectDepartmentCommand
        {
            get
            {
                if (_SelectDepartmentCommand == null)
                {
                    _SelectDepartmentCommand = new ListenerCommand<Department>(SelectDepartment);
                }
                return _SelectDepartmentCommand;
            }
        }

        public void SelectDepartment(Department parameter)
        {
            this.User.Department = parameter;
        }

        #endregion

        #region ModeProperty
        private string _Mode;

        public string Mode
        {
            get
            { return _Mode; }
            set
            {
                if (_Mode == value)
                    return;
                _Mode = value;
                RaisePropertyChanged();
            }
        }
        #endregion




        private ViewModelCommand _SendCommand;

        public ViewModelCommand SendCommand
        {
            get
            {
                if (_SendCommand == null)
                {
                    _SendCommand = new ViewModelCommand(SendAsync);
                }
                return _SendCommand;
            }
        }

        public async void SendAsync()
        {
            string  a = await this.User.ExistUser();

            string process =await service.EditUserAsync(User);
            if (process == "success")
            {
                MessageBox.Show("データ更新に成功しました。", "情報");
                Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
            }
            else
            {
                MessageBox.Show("データ更新に失敗しました。正しい値を入力してください", "エラー");
            }
        }

        

        public EditUserViewModel(User user ,String mode)
        {
            this.User = user;
            this.Mode = mode;

            if(mode == "Add")
            {
                service =new EditUserPostService();
            }
            else if(mode == "Put")
            {
                service = new EditUserPutService();
            }

        }

        public EditUserViewModel()
        {
        }

        public async void Initialize()
        {
            IRestService service = new RestService();
            this.Departments = await service.GetDepartmentsAsync();
        }

    }
}
