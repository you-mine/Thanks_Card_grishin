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
using System.Windows;

namespace LivetApp1.ViewModels
{
    public class ContentViewModel : ViewModel
    {
        #region テンプレ
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
        #endregion

        #region Help1Property

        private Help1Content _Help1Content;

        public Help1Content Help1Content
        {
            get
            { return _Help1Content; }
            set
            { 
                if (_Help1Content == value)
                    return;
                _Help1Content = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Help2Proerty

        private Help2Content _Help2Content;

        public Help2Content Help2Content
        {
            get
            { return _Help2Content; }
            set
            { 
                if (_Help2Content == value)
                    return;
                _Help2Content = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region PlaceProperty

        private PlaceContent _PlaceContent;

        public PlaceContent PlaceContent
        {
            get
            { return _PlaceContent; }
            set
            { 
                if (_PlaceContent == value)
                    return;
                _PlaceContent = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Help1List

        private List<string> _help1Content;

        public List<string> help1Content
        {
            get
            { return _help1Content; }
            set
            { 
                if (_help1Content == value)
                    return;
                _help1Content = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Help2List

        private List<string> _help2Content;

        public List<string> help2Content
        {
            get
            { return _help2Content; }
            set
            { 
                if (_help2Content == value)
                    return;
                _help2Content = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region PlaceList
        
        private List<string> _placeContent;

        public List<string> placeContent
        {
            get
            { return _placeContent; }
            set
            { 
                if (_placeContent == value)
                    return;
                _placeContent = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Help1Post

        private ViewModelCommand _Help1PostCommand;

        public ViewModelCommand Help1PostCommand
        {
            get
            {
                if (_Help1PostCommand == null)
                {
                    _Help1PostCommand = new ViewModelCommand(Help1Post);
                }
                return _Help1PostCommand;
            }
        }

        public async void Help1Post()
        {
            string process = await service.EditUserAsync();
            if (process == "success")
            {
                MessageBox.Show("データ作成に成功しました。", "情報");
                Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
            }
            else
            {
                MessageBox.Show("データ更新に失敗しました。正しい値を入力してください", "エラー");
            }
        }

        public Help1ContentViewModel(Help1Content help1Content, String mode)
        {
            this.Help1Content = help1Content;

            if (mode == "Add")
            {
                service = new EditUserPostService();
            }
            else if (mode == "Put")
            {
                service = new EditUserPutService();
            }

        }

        public EditUserViewModel()
        {
        }

        #endregion

        #region Help1Put

        private ViewModelCommand _Help1PutCommand;

        public ViewModelCommand Help1PutCommand
        {
            get
            {
                if (_Help1PutCommand == null)
                {
                    _Help1PutCommand = new ViewModelCommand(Help1Put);
                }
                return _Help1PutCommand;
            }
        }

        public void Help1Put()
        {
            
        }

        #endregion

        #region Help1Delete

        private ViewModelCommand _Help1DeleteCommand;

        public ViewModelCommand Help1DeleteCommand
        {
            get
            {
                if (_Help1DeleteCommand == null)
                {
                    _Help1DeleteCommand = new ViewModelCommand(Help1Delete);
                }
                return _Help1DeleteCommand;
            }
        }

        public void Help1Delete()
        {
            
        }

        #endregion

        #region Help2Post

        private ViewModelCommand _Help2PostCommand;

        public ViewModelCommand Help2PostCommand
        {
            get
            {
                if (_Help2PostCommand == null)
                {
                    _Help2PostCommand = new ViewModelCommand(Help2Post);
                }
                return _Help2PostCommand;
            }
        }

        public void Help2Post()
        {
            
        }

        #endregion

        #region Help2Put

        private ViewModelCommand _Help2PutCommand;

        public ViewModelCommand Help2PutCommand
        {
            get
            {
                if (_Help2PutCommand == null)
                {
                    _Help2PutCommand = new ViewModelCommand(Help2Put);
                }
                return _Help2PutCommand;
            }
        }

        public void Help2Put()
        {
            
        }

        #endregion

        #region Help2Delete

        private ViewModelCommand _Help2DeleteCommand;

        public ViewModelCommand Help2DeleteCommand
        {
            get
            {
                if (_Help2DeleteCommand == null)
                {
                    _Help2DeleteCommand = new ViewModelCommand(Help2Delete);
                }
                return _Help2DeleteCommand;
            }
        }

        public void Help2Delete()
        {

        }

        #endregion

        #region PlacePost

        private ViewModelCommand _PlacePostCommand;

        public ViewModelCommand PlacePostCommand
        {
            get
            {
                if (_PlacePostCommand == null)
                {
                    _PlacePostCommand = new ViewModelCommand(PlacePost);
                }
                return _PlacePostCommand;
            }
        }

        public void PlacePost()
        {

        }

        #endregion

        #region PlacePut

        private ViewModelCommand _PlacePutCommand;

        public ViewModelCommand PlacePutCommand
        {
            get
            {
                if (_PlacePutCommand == null)
                {
                    _PlacePutCommand = new ViewModelCommand(PlacePut);
                }
                return _PlacePutCommand;
            }
        }

        public void PlacePut()
        {
            
        }

        #endregion

        #region PlaceDelete

        private ViewModelCommand _PlaceDeleteCommand;

        public ViewModelCommand PlaceDeleteCommand
        {
            get
            {
                if (_PlaceDeleteCommand == null)
                {
                    _PlaceDeleteCommand = new ViewModelCommand(PlaceDelete);
                }
                return _PlaceDeleteCommand;
            }
        }

        public void PlaceDelete()
        {
             
        }

        #endregion

        public void Initialize()
        {

        }
    }
}
