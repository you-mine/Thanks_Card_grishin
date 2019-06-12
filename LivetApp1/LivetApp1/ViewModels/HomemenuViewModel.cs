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
    public class HomemenuViewModel : ViewModel
    {

        IRestService service = new RestService();

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
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault((w) => w.IsActive);
            window.Hide();
            var message = new TransitionMessage(typeof(Views.Ranking), new RankingViewModel(), TransitionMode.Modal, "Ranking");
            Messenger.Raise(message);
            window.Show();
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
            Initialize();
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
            bool IsAdmin = Services.SessionService.Instance.AuthorizedUser.IsAdmin;
            if (IsAdmin)
            {
                var message = new TransitionMessage(typeof(Views.AdminMenu), new AdminMenuViewModel(), TransitionMode.Modal, "AdminMenu");
                Messenger.Raise(message);
                Initialize();
            }
            else
            {
                MessageBox.Show("管理者専用機能です", "情報");
            }

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
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Logout"));
        }



        #endregion
        
        #region CreateCard
        private ViewModelCommand _CreateCradCommand;

        public ViewModelCommand CreateCradCommand
        {
            get
            {
                if (_CreateCradCommand == null)
                {
                    _CreateCradCommand = new ViewModelCommand(CreateCrad);
                }
                return _CreateCradCommand;
            }
        }

        public void CreateCrad()
        {
            var message = new TransitionMessage(typeof(Views.CreateCard), new CreateCardViewModel(), TransitionMode.Modal, "CreateCard");
            Messenger.Raise(message);
            Initialize();
        }
        #endregion

        #region CreateCard2

        private ViewModelCommand _CreateCard2Command;

        public ViewModelCommand CreateCard2Command
        {
            get
            {
                if (_CreateCard2Command == null)
                {
                    _CreateCard2Command = new ViewModelCommand(CreateCard2);
                }
                return _CreateCard2Command;
            }
        }

        public void CreateCard2()
        {
            var message = new TransitionMessage(typeof(Views.CreateCard2), new CreateCard2ViewModel(), TransitionMode.Modal, "CreateCard2");
            Messenger.Raise(message);
            Initialize();
        }

        #endregion

        #region Cardsプロパティ

        private List<ThanksCard> _Cards;

        public List<ThanksCard> Cards
        {
            get
            { return _Cards; }
            set
            { 
                if (_Cards == value)
                    return;
                _Cards = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region FromCards

        private List<ThanksCard> _From;

        public List<ThanksCard> From
        {
            get
            { return _From; }
            set
            { 
                if (_From == value)
                    return;
                _From = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region ToCards

        private List<ThanksCard> _To;

        public List<ThanksCard> To
        {
            get
            { return _To; }
            set
            { 
                if (_To == value)
                    return;
                _To = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region RepresentativeCardsプロパティ

        private List<ThanksCard> _RepresentativeCards;

        public List<ThanksCard> RepresentativeCards
        {
            get
            { return _RepresentativeCards; }
            set
            { 
                if (_RepresentativeCards == value)
                    return;
                _RepresentativeCards = value;
                RaisePropertyChanged();
            }
        }

        #endregion 

        

        public async void Initialize()
        {
            
            Cards = await service.GetCardsAsync();

            User AuthorizedUser = SessionService.Instance.AuthorizedUser;

            From = Cards.Where(x => x.FromId == AuthorizedUser.Id).ToList();

            To = Cards.Where(x => x.ToId == AuthorizedUser.Id).ToList();
            
            RepresentativeCards = Cards.Where(x => x.IsRepresentative == true).ToList();

        }
    }
}
