using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace LivetApp1.Models
{
    public class AnalyzeDtoD : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        #region ToDepartmentName
        private string _ToDepartmentName;

        public string ToDepartmentName
        {
            get
            { return _ToDepartmentName; }
            set
            {
                if (_ToDepartmentName == value)
                    return;
                _ToDepartmentName = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region FromDepartmentName

        private string _FromDepartmentName;

        public string FromDepartmentName
        {
            get
            { return _FromDepartmentName; }
            set
            { 
                if (_FromDepartmentName == value)
                    return;
                _FromDepartmentName = value;
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
