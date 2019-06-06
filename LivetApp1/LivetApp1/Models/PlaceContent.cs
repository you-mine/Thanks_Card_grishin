using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace LivetApp1.Models
{
    public class PlaceContent : NotificationObject
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

        #region PlaceProperty

        private string _Place;

        public string Place
        {
            get
            { return _Place; }
            set
            { 
                if (_Place == value)
                    return;
                _Place = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}
