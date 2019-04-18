using Prism.Commands;
using Prism.Events;
using System;
using TokenAuthentication.Example.Core;
using TokenAuthentication.Example.Events;

namespace TokenAuthentication.Example.ViewModels
{
    public class UserProfileViewModel : BaseViewModel
    {
        public UserProfileViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            LogoutCommand = new DelegateCommand(Logout);
            SwitchUserCommand = new DelegateCommand(SwitchUser);

            EventAggregator.GetEvent<UserSessionCreatedEvent>().Subscribe(args => {
                FullName = args.Portal.User.FullName;
                UserName = args.Portal.User.UserName;
                ProfilePicture = args.Portal.User.ThumbnailUri;
            });
        }

        private Uri _profilePicture;
        public Uri ProfilePicture
        {
            get { return _profilePicture; }
            set { SetProperty(ref _profilePicture, value); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { SetProperty(ref _fullName, value); }
        }

        public DelegateCommand LogoutCommand { get; }

        private void Logout()
        {
            EventAggregator.GetEvent<UserSessionEndedEvent>().Publish(
                new UserSessionEndedMessage(UserSessionEndedMessage.SessionEndingType.Logout));
        }

        public DelegateCommand SwitchUserCommand { get; }

        private void SwitchUser()
        {
            EventAggregator.GetEvent<UserSessionEndedEvent>().Publish(
                new UserSessionEndedMessage(UserSessionEndedMessage.SessionEndingType.SwitchUser));
        }
    }
}
