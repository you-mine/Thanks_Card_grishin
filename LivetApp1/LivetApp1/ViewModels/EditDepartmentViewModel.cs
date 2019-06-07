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

namespace LivetApp1.ViewModels
{
    public class EditDepartmentViewModel : ViewModel
    {
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

        #region EditDepartment

        private Department _EditDepartment;

        public Department EditDepartment
        {
            get
            { return _EditDepartment; }
            set
            {
                if (_EditDepartment == value)
                    return;
                _EditDepartment = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region SelectCommand

        private ListenerCommand<Department> _SelectCommand;

        public ListenerCommand<Department> SelectCommand
        {
            get
            {
                if (_SelectCommand == null)
                {
                    _SelectCommand = new ListenerCommand<Department>(Select);
                }
                return _SelectCommand;
            }
        }

        public void Select(Department parameter)
        {
            this.EditDepartment = parameter;
        }

        #endregion

        public void Initialize()
        {
        }

    }
}
