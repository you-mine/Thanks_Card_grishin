using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using LivetApp1.Models;
using LivetApp1.Services;

namespace LivetApp1.ViewModels
{
    public class EditUserViewModel : ViewModel
    {

        IEditUserService service = null;

        /* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
         *  
         * を使用してください。
         * 
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         * 
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

        /* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

        /* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         * 
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         * 
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         * 
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

        /* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         * 
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */




        #region DepartmentProperrty
        private List<Department> _Departments;

        public List<Department> Departments
        {
            get
            { return _Departments; }
            set
            { 
                if (_Departments == value)
                    return;
                _Departments = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Department Command
        private ListenerCommand<Department> _SelectDepartmentCommand;


        public ListenerCommand<Department> SelectDepartmentCommand
        {
            get
            {
                if (_SelectDepartmentCommand == null)
                {
                    _SelectDepartmentCommand = new ListenerCommand<Department>(SelectDepartment);
                }
                return _SelectDepartmentCommand;
            }
        }

        public void SelectDepartment(Department parameter)
        {
            this.User.Department = parameter;
        }

        #endregion


        private ViewModelCommand _SendCommand;

        public ViewModelCommand SendCommand
        {
            get
            {
                if (_SendCommand == null)
                {
                    _SendCommand = new ViewModelCommand(Send);
                }
                return _SendCommand;
            }
        }

        public void Send()
        {
            service.EditUserAsync(this.User);
        }




        public EditUserViewModel(User user ,String mode)
        {
            this.User = user;

            if(mode == "Add")
            {
                service =new EditUserPostService();
            }
            else if(mode == "Put")
            {
                service = new EditUserPutService();
            }

        }

        public EditUserViewModel()
        {
        }

        public async void Initialize()
        {
            IRestService service = new RestService();
            this.Departments = await service.GetDepartmentsAsync();
        }


        #region UserProperty
        private User _User;

        public User User
        {
            get
            { return _User; }
            set
            {
                if (_User == value)
                    return;
                _User = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        private ViewModelCommand _EditUserCommand;

        public ViewModelCommand EditUserCommand
        {
            get
            {
                if (_EditUserCommand == null)
                {
                    _EditUserCommand = new ViewModelCommand(EditUser);
                }
                return _EditUserCommand;
            }
        }

        public async void EditUser()
        {
          String process = await this.User.EditAsync();
            //TODO 処理の成功可否処理の追加
        }


    }
}
