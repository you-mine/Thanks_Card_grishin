using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LivetApp1.Services;
using Livet;
using System.Threading.Tasks;

namespace LivetApp1.Models
{
    public class User : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */
        
        #region IdProperty
        private long _Id;

        public long Id
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

        #region NameProperty
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

        #region PasswordProperty
        private string _Password;

        public string Password
        {
            get
            { return _Password; }
            set
            { 
                if (_Password == value)
                    return;
                _Password = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region IsAdminProperty
        private bool _IsAdmin;
    
        public bool IsAdmin
        {
            get
            { return _IsAdmin; }
            set
            { 
                if (_IsAdmin == value)
                    return;
                _IsAdmin = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ログインメソッド
        public async Task<User> LogonAsync()
        {
            IRestService rest = new RestService();
            User authorizedUser = await rest.LogonAsync(this);
            return authorizedUser;
        }
        #endregion

        #region ユーザー情報編集メソッド
        public async Task<String> EditAsync()
        {
            IRestService rest = new RestService();
            return await rest.PutUserAsync(this);
        }
        #endregion
    }
}
