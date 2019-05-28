using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace LivetApp1.Models
{
    public class Department : NotificationObject
    {
        #region Id Proparty
        private int _Id;

        public int Id
        {
            get
            { return _Id; }
            set
            { 
                if (_Id == value)
                    return;
                _Id = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CD Proparty

        private string _CD;

        public string CD
        {
            get
            { return _CD; }
            set
            { 
                if (_CD == value)
                    return;
                _CD = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region DepartmentName Proparty

        private string _DepartmentName;

        public string DepartmentName
        {
            get
            { return _DepartmentName; }
            set
            { 
                if (_DepartmentName == value)
                    return;
                _DepartmentName = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region SectionName Proparty

        private string _SectionName;

        public string SectionName
        {
            get
            { return _SectionName; }
            set
            { 
                if (_SectionName == value)
                    return;
                _SectionName = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}
