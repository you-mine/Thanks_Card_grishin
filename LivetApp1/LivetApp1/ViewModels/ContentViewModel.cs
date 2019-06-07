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
using System.Windows;
using LivetApp1.Services;

namespace LivetApp1.ViewModels
{
    public class ContentViewModel : ViewModel
    {
        IContentServise service = null;

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

        #region ContentProperty

        private Content _content;

        public Content content
        {
            get
            { return _content; }
            set
            {
                if (_content == value)
                    return;
                _content = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Close

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

        #region Help1List

        private List<Content> _help1Content;

        public List<Content> help1Content
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

        private List<Content> _help2Content;

        public List<Content> help2Content
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

        private List<Content> _placeContent;

        public List<Content> placeContent
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
            string result = await service.Put(content);
            if (result == "success")
            {
                MessageBox.Show("データ更新に成功しました。", "情報");
                Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
            }
            else
            {
                MessageBox.Show("データ更新に失敗しました。正しい値を入力してください", "エラー");
            }
        }
        #endregion

        #region DeleteCommand

        private ViewModelCommand _DeleteCommand;

        public ViewModelCommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new ViewModelCommand(Delete);
                }
                return _DeleteCommand;
            }
        }

        public void Delete()
        {
            service.Delete(content);
        }

        #endregion

        public void ChangeToPlace()
        {
            this.service = new PlaceContentService();
        }

        public void ChangeToThanks1()
        {
            this.service = new Help1ContentService();
        }

        public void ChangeToThank2()
        {
            this.service = new Help2ContentService();
        }

        public async void Initialize()
        {
            service = new Help1ContentService();
            help1Content = await service.Get();
            service = new Help2ContentService();
            help2Content = await service.Get();
            service = new PlaceContentService();
            placeContent = await service.Get();

        }
    }
}
