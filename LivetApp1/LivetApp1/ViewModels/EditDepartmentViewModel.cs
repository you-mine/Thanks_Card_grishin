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

using LivetApp1.Services;

using LivetApp1.Models;
using System.Windows;

namespace LivetApp1.ViewModels
{
    public class EditDepartmentViewModel : ViewModel
    {
        private IEditDepartment service = null;

        public EditDepartmentViewModel() { }
        
        public EditDepartmentViewModel(Department department,string mode)
        {
            if(mode == "put")
            {
                this.Department = department;
                service = new PutDepartmentService();
            }
            else if (mode == "post")
            {
                this.Department = department;
                service = new PostDepartmentService();
            }
        }

        #region Department
        private Department _Department;

        public Department Department
        {
            get
            { return _Department; }
            set
            {
                if (_Department == value)
                    return;
                _Department = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region SendCommand
        private ListenerCommand<Department> _SendCommand;

        public ListenerCommand<Department> SendCommand
        {
            get
            {
                if (_SendCommand == null)
                {
                    _SendCommand = new ListenerCommand<Department>(Send);
                }
                return _SendCommand;
            }
        }

        public async void Send(Department parameter)
        {
            string error = Inputcheck();

            if (string.IsNullOrEmpty(error))
            {
                string result = await service.EditDepartment(parameter);

                if (result == "success")
                {
                    MessageBox.Show("データ更新に成功しました。", "情報");
                    Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
                }
                else
                {
                    MessageBox.Show("CDが重複している為データが更新できませんでした。", "エラー！");
                }
            }
            else
            {
                MessageBox.Show(error+"を入力してください", "情報");
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

        private string Inputcheck()
        {
            string result = null;
            if (string.IsNullOrEmpty(this.Department.CD))
            {
                result += "コード";
            }

            return result;
        }

        public void Initialize()
        {
        }
        
    }
}
