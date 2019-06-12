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
    public class DepartmentViewViewModel : ViewModel
    {
        IRestService service = new RestService();

        #region Departments

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

        #region PutCommand

        private ListenerCommand<Department> _PutCommand;

        public ListenerCommand<Department> PutCommand
        {
            get
            {
                if (_PutCommand == null)
                {
                    _PutCommand = new ListenerCommand<Department>(Select);
                }
                return _PutCommand;
            }
        }

        public async void Select(Department parameter)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault((w) => w.IsActive);
            window.Hide();
            var message = new TransitionMessage(typeof(Views.EditDepartment), new EditDepartmentViewModel(parameter,"put"), TransitionMode.Modal, "Edit");
            Messenger.Raise(message);
            window.Show();
            this.Departments = await service.GetDepartmentsAsync();
        }

        #endregion

        #region PostCommand

        private ViewModelCommand _PostCommand;

        public ViewModelCommand PostCommand
        {
            get
            {
                if (_PostCommand == null)
                {
                    _PostCommand = new ViewModelCommand(Post);
                }
                return _PostCommand;
            }
        }

        public async void Post()
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault((w) => w.IsActive);
            window.Hide();
            var message = new TransitionMessage(typeof(Views.EditDepartment), new EditDepartmentViewModel(new Department(), "post"), TransitionMode.Modal, "Edit");
            Messenger.Raise(message);
            window.Show();
            this.Departments = await service.GetDepartmentsAsync();
        }

        #endregion

        #region DeleteCommand

        private ListenerCommand<Department> _DeleteCommand;

        public ListenerCommand<Department> DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new ListenerCommand<Department>(Delete);
                }
                return _DeleteCommand;
            }
        }

        public async void Delete(Department parameter)
        {
            string result = await service.DeleteDepartment(parameter);
            if(result == "success")
            {
                MessageBox.Show("データを削除しました", "情報");
            }
            else
            {
                MessageBox.Show("データを削除に失敗しました", "情報");
            }
            this.Departments = await service.GetDepartmentsAsync();
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



        public async void Initialize()
        {
            this.Departments = await service.GetDepartmentsAsync();
        }

    }
}
