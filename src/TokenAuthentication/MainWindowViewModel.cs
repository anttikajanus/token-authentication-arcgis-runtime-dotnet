using Prism.Events;
using TokenAuthentication.Example.Core;
using TokenAuthentication.Example.Events;

namespace TokenAuthentication.Example
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            Title = "ArcGIS Portal authentication";
            RequiresLogin = true;

            EventAggregator.GetEvent<UserSessionCreatedEvent>().Subscribe(args => { RequiresLogin = false; });
            EventAggregator.GetEvent<UserSessionEndedEvent>().Subscribe(args => { RequiresLogin = true; });
        }

        private bool _reqiresLogin;
        public bool RequiresLogin
        {
            get { return _reqiresLogin; }
            set { SetProperty(ref _reqiresLogin, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
