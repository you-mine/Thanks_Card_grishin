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
using System.Windows;

namespace LivetApp1.ViewModels
{
    public class CreateCard2ViewModel : ViewModel
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

        #region　Close

        private ViewModelCommand _CloseCommand;

        public ViewModelCommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = new ViewModelCommand(Close);
                }
                return _CloseCommand;
            }
        }

        public void Close()
        {
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
        }

        #endregion

        #region PlaceProperty

        private List<Content> _Place;

        public List<Content> Place
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

        #region SelectCommand
        private ListenerCommand<User> _SelectCommand;

        public ListenerCommand<User> SelectCommand
        {
            get
            {
                if (_SelectCommand == null)
                {
                    _SelectCommand = new ListenerCommand<User>(Select);
                }
                return _SelectCommand;
            }
        }

        public void Select(User parameter)
        {
            this.ThanksCard.To = parameter;
        }


        #endregion

        #region Help1

        private List<Content> _Help1;

        public List<Content> Help1
        {
            get
            { return _Help1; }
            set
            { 
                if (_Help1 == value)
                    return;
                _Help1 = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Help2

        private List<Content> _Help2;

        public List<Content> Help2
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

        #region Send
        private ViewModelCommand _SendCommand;

        public ViewModelCommand SendCommand
        {
            get
            {
                if (_SendCommand == null)
                {
                    _SendCommand = new ViewModelCommand(SendAsync);
                }
                return _SendCommand;
            }
        }

        public async void SendAsync()
        {
            string error = Inputcheck();
            if (string.IsNullOrEmpty(error))
            {
                ThanksCard.ThanksCount = 1;
                ThanksCard.Body = Message1 + "で" + Message2 + "の" + Message3 + "をしてくれてありがとうございました。";
                ThanksCard.FromId = ThanksCard.From.Id;
                ThanksCard.ToId = ThanksCard.To.Id;
                ThanksCard.PostDate = DateTime.Now.Date;
                ThanksCard.From = null;
                ThanksCard.To = null;
                var result = await this.ThanksCard.CreateCard();
                if(result == "success")
                {
                    MessageBox.Show("感謝カードを送信しました。");
                }
                else
                {
                    MessageBox.Show("感謝カードの送信に失敗しました。");
                }
                Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Authorized"));
            }
            else
            {
                MessageBox.Show(error, "エラー");
            }

        }

        #endregion

        #region ThanksCard
        private ThanksCard _ThanksCard;

        public ThanksCard ThanksCard
        {
            get
            { return _ThanksCard; }
            set
            { 
                if (_ThanksCard == value)
                    return;
                _ThanksCard = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Users

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

        #region Message1
        private string _Message1;

        public string Message1
        {
            get
            { return _Message1; }
            set
            { 
                if (_Message1 == value)
                    return;
                _Message1 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Message2


        private string _Message2;

        public string Message2
        {
            get
            { return _Message2; }
            set
            { 
                if (_Message2 == value)
                    return;
                _Message2 = value;
                RaisePropertyChanged();
            }
        }





        #endregion

        #region Message3


        private string _Message3;

        public string Message3
        {
            get
            { return _Message3; }
            set
            { 
                if (_Message3 == value)
                    return;
                _Message3 = value;
                RaisePropertyChanged();
            }
        }


        #endregion


        public void Initialize()
        {
            this.UpdateData();
        }

        private async void UpdateData()
        {
            //this.Users = new List<User>();
            ShowUserService service = new ShowUserService();
            List<User> users = await service.ShowUserAsync();
            this.Users = users; //プロパティに入れる。
            this.ThanksCard = new ThanksCard();
            SessionService session = SessionService.Instance;
            this.ThanksCard.From = session.AuthorizedUser;
            IContentServise servise = new Help1ContentService();
            this.Help1 = await servise.Get();
            servise = new Help2ContentService();
            this.Help2 = await servise.Get();
            servise = new PlaceContentService();
            this.Place = await servise.Get();
        }
        
        private string Inputcheck()
        {
            string error = "";
                if (string.IsNullOrEmpty(ThanksCard.Title))
                {
                    error += "タイトルを入力してください\n";
                }
                else if (ThanksCard.Title.Length > 20)
                {
                    error += "タイトルが長すぎます。20文字以内で入力してください。\n";
                }

            if (string.IsNullOrEmpty(Message1))
            {
                error += "場所を入力してください\n";
            }

            if (string.IsNullOrEmpty(Message2))
            {
                error += "内容1を入力してください\n";
            }

            if (string.IsNullOrEmpty(Message3))
            {
                error += "内容2を入力してください\n";
            }

            if(ThanksCard.To == null)
            {
                error += "宛先を選択してください\n";
            }

            return error;
        }
    }
}
