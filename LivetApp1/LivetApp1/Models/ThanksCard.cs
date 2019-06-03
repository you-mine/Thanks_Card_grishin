using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using LivetApp1.Services;

namespace LivetApp1.Models
{
    public class ThanksCard : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */
        #region CardIdProperty
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

        #region FromIDProparty

        private int _FromId;

        public int FromId
        {
            get
            { return _FromId; }
            set
            { 
                if (_FromId == value)
                    return;
                _FromId = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region ToIDProparty

        private int _ToId;

        public int ToId
        {
            get
            { return _ToId; }
            set
            { 
                if (_ToId == value)
                    return;
                _ToId = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region CardToProperty

        private User _To;

        public User To
        {
            get
            { return _To; }
            set
            {
                if (_To == value)
                    return;
                _To = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region CardFromProperty

        private User _From;

        public User From
        {
            get
            { return _From; }
            set
            { 
                if (_From == value)
                    return;
                _From = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region CardPostDateProperty

        private DateTime _PostDate;

        public DateTime PostDate
        {
            get
            { return _PostDate; }
            set
            { 
                if (_PostDate == value)
                    return;
                _PostDate = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region CardTitleProperty

        private string _Title;

        public string Title
        {
            get
            { return _Title; }
            set
            { 
                if (_Title == value)
                    return;
                _Title = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region CardBodyProperty

        private string _Body;

        public string Body
        {
            get
            { return _Body; }
            set
            { 
                if (_Body == value)
                    return;
                _Body = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region CardThanksCountProperty

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

        #region IsReadedProperty

        private bool _IsReaded;

        public bool IsReaded
        {
            get
            { return _IsReaded; }
            set
            { 
                if (_IsReaded == value)
                    return;
                _IsReaded = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region IsReadedAdmin

        private bool _IsReadedAdmin;

        public bool IsReadedAdmin
        {
            get
            { return _IsReadedAdmin; }
            set
            { 
                if (_IsReadedAdmin == value)
                    return;
                _IsReadedAdmin = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region IsRepresentative

        private bool _IsRepresentative;

        public bool IsRepresentative
        {
            get
            { return _IsRepresentative; }
            set
            { 
                if (_IsRepresentative == value)
                    return;
                _IsRepresentative = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region カード送信メソッド
        public async Task<ThanksCard> CreateCardAsync()
        {
            IRestService rest = new RestService();
            ThanksCard authorizedThanksCard = await rest.CreateCardAsync(this);
            return authorizedThanksCard;
        }
        #endregion

        #region カード更新メソッド
        public void PutCard()
        {
            IRestService rest = new RestService();
            rest.PutCard(this);
        }
        #endregion

    }
}
