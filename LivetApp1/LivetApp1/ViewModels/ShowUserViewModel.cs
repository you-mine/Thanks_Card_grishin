﻿using System;
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
using LivetApp1.Services;
using LivetApp1.Models;

namespace LivetApp1.ViewModels
{
    public class ShowUserViewModel : ViewModel
    {

        public void Initialize()
        {
            _Users = new List<User>();
        }

        #region UsersProperty

        private List<User> _Users;

        public List<User> Users
        {
            get
            { return _Users; }
            set
            { 
                if (_Users == value)
                    return;
                _Users = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        #region ShowUserCommand

        private ViewModelCommand _ShowUserCommand;

        public ViewModelCommand ShowUserCommand
        {
            get
            {
                if (_ShowUserCommand == null)
                {
                    _ShowUserCommand = new ViewModelCommand(ShowUser);
                }
                return _ShowUserCommand;
            }
        }

        async public void ShowUser()
        {
            ShowUserService service = new ShowUserService();
            List<User> users = await service.ShowUserAsync();


            if (users != null) // 全件取得成功
            {
                this.Users = users; //プロパティに入れる。
            }
            else // Logon 失敗
            {
                System.Diagnostics.Debug.WriteLine("データの取得に失敗しました");
            }

        }

        #endregion

        #region EditUserCommand

        private ListenerCommand<User> _EditUserCommand;

        public ListenerCommand<User> EditUserCommand
        {
            get
            {
                if (_EditUserCommand == null)
                {
                    _EditUserCommand = new ListenerCommand<User>(EditUser);
                }
                return _EditUserCommand;
            }
        }

        public void EditUser(User parameter)
        {
            var message = new TransitionMessage(typeof(Views.EditUser), new EditUserViewModel(parameter), TransitionMode.Modal, "ShowEdit");
            Messenger.Raise(message);
        }
        #endregion

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


    }
}
