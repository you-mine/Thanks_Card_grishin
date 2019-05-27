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

namespace LivetApp1.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region MyMessageProperty
        private string _MyMessage;

        public string MyMessage
        {
            get
            { return _MyMessage; }
            set
            {
                if (_MyMessage == value)
                    return;
                _MyMessage = value;
                RaisePropertyChanged();
                System.Diagnostics.Debug.WriteLine("MyMessage: " + this.MyMessage); //動作確認用。本来はこの行は必要ありません。

            }
        }
        #endregion
        　
        #region Mydate Property
        private DateTime _MyDate;

        public DateTime MyDate
        {
            get
            { return _MyDate; }
            set
            { 
                if (_MyDate == value)
                    return;
                _MyDate = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region Text1Property
        private string _Text1;

        public string Text1
        {
            get
            { return _Text1; }
            set
            { 
                if (_Text1 == value)
                    return;
                _Text1 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Text2Property
        private string _Text2;

        public string Text2
        {
            get
            { return _Text2; }
            set
            { 
                if (_Text2 == value)
                    return;
                _Text2 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Text3Property
        private string _Text3;

        public string Text3
        {
            get
            { return _Text3; }
            set
            { 
                if (_Text3 == value)
                    return;
                _Text3 = value;
                RaisePropertyChanged();
            }
        }
        #endregion




        #region TestCommand
        private ViewModelCommand _TestCommand;

        public ViewModelCommand TestCommand
        {
            get
            {
                if (_TestCommand == null)
                {
                    _TestCommand = new ViewModelCommand(Test);
                }
                return _TestCommand;
            }
        }

        public void Test()
        {
            Text3 = Text1 + Text2;
        }
        #endregion

        
        #region Test2Command
        private ListenerCommand<string> _Test2Command;

        public ListenerCommand<string> Test2Command
        {
            get
            {
                if (_Test2Command == null)
                {
                    _Test2Command = new ListenerCommand<string>(Test2);
                }
                return _Test2Command;
            }
        }

        public void Test2(string parameter)
        {
            System.Diagnostics.Debug.WriteLine("Test2Command が呼ばれました。パラメータは「" + parameter + "」です。");
        }
        #endregion

        public void Initialize()
        {
            //var message = new TransitionMessage(typeof(Views.Logon), new LogonViewModel(), TransitionMode.Modal, "ShowLogon");
            //var message = new TransitionMessage(typeof(Views.ShowUser), new ShowUserViewModel(), TransitionMode.Modal, "ShowUsers");
            //Messenger.Raise(message);
            this.MyMessage = "こんにちは";
        }

        #region なんかテンプレ
        /* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom    : ViewModelCommand
         *  lvcomn   : ViewModelCommand(CanExecute無)
         *  llcom    : ListenerCommand(パラメータ有のコマンド)
         *  llcomn   : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop    : 変更通知プロパティ
         *  lsprop   : 変更通知プロパティ(ショートバージョン)
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
        #endregion
    }
}
