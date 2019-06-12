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

        #region DepartmentIdProparty
        private int _DepartmentId;

        public int DepartmentId
        {
            get
            { return _DepartmentId; }
            set
            { 
                if (_DepartmentId == value)
                    return;
                _DepartmentId = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region DepartmentProparty
        private Department _Department;

        public Department Department
        {
            get
            { return _Department; }
            set
            { 
                if (_Department == value)
                    return;

                _Department = value;
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

        #region UsernameProperty
        private string _UserName;

        public string UserName
        {
            get
            { return _UserName; }
            set
            { 
                if (_UserName == value)
                    return;
                _UserName = value;
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

        #region HuriganaProperty


        private string _Hurigana;

        public string Hurigana
        {
            get
            { return _Hurigana; }
            set
            { 
                if (_Hurigana == value)
                    return;
                _Hurigana = value;
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

        #region ユーザーネーム重複確認
        public async Task<string> ExistUser()
        {
            IRestService rest = new RestService();
            return await rest.UserNameExist(this.UserName);
        }
        #endregion
    }
}
