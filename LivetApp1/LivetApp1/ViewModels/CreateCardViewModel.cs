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

using LivetApp1.Models;
using LivetApp1.Services;
using System.Windows;

namespace LivetApp1.ViewModels
{
    public class CreateCardViewModel : ViewModel
    {

        #region プロパティのテンプレ
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

        #region CloseCommand
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

        #region SendCommand
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
                //外部キーのみをエンティティに持たせる。
                ThanksCard.ThanksCount = 1;
                ThanksCard.FromId = ThanksCard.From.Id;
                ThanksCard.ToId = ThanksCard.To.Id;
                ThanksCard.PostDate = DateTime.Now.Date;
                ThanksCard.From = null;
                ThanksCard.To = null;
                var result = await this.ThanksCard.CreateCard();
                if(result == "success")
                {
                    MessageBox.Show("感謝カードを送信しました");
                }
                
                Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Authorized"));
            }
            else
            {
                MessageBox.Show(error,"エラー");
            }

        }
        #endregion

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
            var message = new TransitionMessage(typeof(Views.Logon), new CreateCardViewModel(), TransitionMode.Modal, "");
            Messenger.Raise(message);
            this.ThanksCard = new ThanksCard();
            SessionService session = SessionService.Instance;
            this.ThanksCard.From = session.AuthorizedUser;
        }

        private string Inputcheck()
        {
            string error = "";
            if(ThanksCard.To == null)
            {
                error += "宛先を選択してください\n";
            }
            if (string.IsNullOrEmpty(ThanksCard.Body))
            {
                error += "本文を入力してください\n";
            }
            else if(ThanksCard.Body.Length > 400)
            {
                error += "本文が長すぎます。400文字以内で入力してください。\n";
            }

            if (string.IsNullOrEmpty(ThanksCard.Title))
            {
                error += "タイトルを入力してください\n";
            }
            else if(ThanksCard.Title.Length > 20)
            {
                error += "タイトルが長すぎます。20文字以内で入力してください。\n";
            }

            return error;
        }

    }
}
