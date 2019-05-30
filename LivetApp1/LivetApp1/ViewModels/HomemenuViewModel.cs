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
    public class HomemenuViewModel : ViewModel
    {
        
         
        #region Ranking


        private ViewModelCommand _RankingCommand;


        public ViewModelCommand RankingCommand
        {
            get
            {
                if (_RankingCommand == null)
                {
                    _RankingCommand = new ViewModelCommand(Ranking);
                }
                return _RankingCommand;
            }
        }

        public void Ranking()
        {
            var message = new TransitionMessage(typeof(Views.Ranking), new RankingViewModel(), TransitionMode.Modal, "Ranking");
            Messenger.Raise(message);
        }





        #endregion

        #region Cardview


        private ViewModelCommand _CardviewCommand;


        public ViewModelCommand CardviewCommand
        {
            get
            {
                if (_CardviewCommand == null)
                {
                    _CardviewCommand = new ViewModelCommand(Cardview);
                }
                return _CardviewCommand;
            }
        }

        public void Cardview()
        {
            var message = new TransitionMessage(typeof(Views.Cardview), new CardviewViewModel(), TransitionMode.Modal, "Cardview");
            Messenger.Raise(message);
        }




        #endregion

        #region AdminMenu



        private ViewModelCommand _AdminMenuCommand;

        public ViewModelCommand AdminMenuCommand
        {
            get
            {
                if (_AdminMenuCommand == null)
                {
                    _AdminMenuCommand = new ViewModelCommand(AdminMenu);
                }
                return _AdminMenuCommand;
            }
        }

        public void AdminMenu()
        {
            var message = new TransitionMessage(typeof(Views.AdminMenu), new AdminMenuViewModel(), TransitionMode.Modal, "AdminMenu");
            Messenger.Raise(message);
        }





        #endregion

        #region Logout


        private ViewModelCommand _LogoutCommand;
        

        public ViewModelCommand LogoutCommand
        {
            get
            {
                if (_LogoutCommand == null)
                {
                    _LogoutCommand = new ViewModelCommand(Logout);
                }
                return _LogoutCommand;
            }
        }

        public void Logout()
        {
            SessionService session = SessionService.Instance;
            session.IsAuthorized = false;
            session.AuthorizedUser = null;
            var message = new TransitionMessage(typeof(Views.MainWindow), new MainWindowViewModel(), TransitionMode.Modal, "Logout");
            Messenger.Raise(message);
        }



        #endregion

        public void Initialize()
        {

        }
    }
}
