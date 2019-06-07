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
    public class AdminMenuViewModel : ViewModel
    {
        #region CardViewCommand
        private ViewModelCommand _CardViewCommand;

        public ViewModelCommand CardViewCommand
        {
            get
            {
                if (_CardViewCommand == null)
                {
                    _CardViewCommand = new ViewModelCommand(CardView);
                }
                return _CardViewCommand;
            }
        }

        public void CardView()
        {
            var message = new TransitionMessage(typeof(Views.CardsViewForAdmin), new CardsViewForAdminViewModel(), TransitionMode.Modal, "CardsView");
            Messenger.Raise(message);
        }


        #endregion


        #region Analyze

        private ViewModelCommand _AnalyzeCommand;

        public ViewModelCommand AnalyzeCommand
        {
            get
            {
                if (_AnalyzeCommand == null)
                {
                    _AnalyzeCommand = new ViewModelCommand(Analyze);
                }
                return _AnalyzeCommand;
            }
        }

        public void Analyze()
        {
            var message = new TransitionMessage(typeof(Views.Analyze), new AnalyzeViewModel(), TransitionMode.Modal, "Analyze");
            Messenger.Raise(message);
        }


        #endregion

        #region output



        private ViewModelCommand _outputCommand;


        public ViewModelCommand outputCommand  {
            get
            {
                if (_outputCommand == null)
                {
                    _outputCommand = new ViewModelCommand(output);
                }
                return _outputCommand;
            }
        }

        public void output()
        {
            var message = new TransitionMessage(typeof(Views.Exceloutput), new ExceloutputViewModel(), TransitionMode.Modal, "Exceloutput");
            Messenger.Raise(message);
        }




        #endregion

        #region EditUser


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

        public void EditUser()
        {
            var message = new TransitionMessage(typeof(Views.ShowUser), new ShowUserViewModel(), TransitionMode.Modal, "EditUser");
            Messenger.Raise(message);
        }





        #endregion

        #region EditDepartment


        private ViewModelCommand _EditDepartmentCommand;


        public ViewModelCommand EditDepartmentCommand
        {
            get
            {
                if (_EditDepartmentCommand == null)
                {
                    _EditDepartmentCommand = new ViewModelCommand(EditDepartment);
                }
                return _EditDepartmentCommand;
            }
        }

        public void EditDepartment()
        {
            var message = new TransitionMessage(typeof(Views.DepartmentView), new DepartmentViewViewModel(), TransitionMode.Modal, "EditDepartment");
            Messenger.Raise(message);
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



        #endregion Close








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

        public void Initialize()
        {
        }
    }
}
