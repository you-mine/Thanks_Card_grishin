using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace LivetApp1.Models
{
    public class Ranking : NotificationObject
    {
        #region Rank Property
        private int _Rank;

        public int Rank
        {
            get
            { return _Rank; }
            set
            { 
                if (_Rank == value)
                    return;
                _Rank = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Name Property

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

        #region DepatmentName Property

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

        #region ThanksCount Property

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
