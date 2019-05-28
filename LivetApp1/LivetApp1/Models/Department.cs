using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace LivetApp1.Models
{
    public class Department : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         * 
         */

        #region IdProperty
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

        #region CDProperty
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

        #region DepartmentName
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

        #region SectionName
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
