using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace LivetApp1.Models
{
    public class Term : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        #region Term1
        private DateTime _Term1 = DateTime.Today;

        public DateTime Term1
        {
            get
            { return _Term1; }
            set
            {
                if (_Term1 == value)
                    return;
                _Term1 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Term2

        private DateTime _Term2 = DateTime.Today;

        public DateTime Term2
        {
            get
            { return _Term2; }
            set
            { 
                if (_Term2 == value)
                    return;
                _Term2 = value;
                RaisePropertyChanged();
            }
        }

        #endregion


    }
}
