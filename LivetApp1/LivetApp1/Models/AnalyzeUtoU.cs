using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace LivetApp1.Models
{
    public class AnalyzeUtoU : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        #region Name

        private string _Name;

        public string Name
        {
            get
            { return _Name; }
            set
            { 
                if (_Name == value)
                    return;
                _Name = value;
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

        #region ThanksCount

        private int _ThanksCount;

        public int ThanksCount
        {
            get
            { return _ThanksCount; }
            set
            { 
                if (_ThanksCount == value)
                    return;
                _ThanksCount = value;
                RaisePropertyChanged();
            }
        }

        #endregion


    }
}
