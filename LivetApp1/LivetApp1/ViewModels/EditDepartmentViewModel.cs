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

        

        public void Initialize()
        {
        }

    }
}
