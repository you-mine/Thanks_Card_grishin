using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace LivetApp1.Models
{
    public class Help2Content : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
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

        #region Help2Property
    
        private string _Help2;

        public string Help2
        {
            get
            { return _Help2; }
            set
            { 
                if (_Help2 == value)
                    return;
                _Help2 = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}
