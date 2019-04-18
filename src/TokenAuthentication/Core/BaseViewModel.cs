using Prism.Events;
using Prism.Mvvm;
using System;

namespace TokenAuthentication.Example.Core
{
    public class BaseViewModel : BindableBase
    {
        public BaseViewModel(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        protected IEventAggregator EventAggregator { get; }
    }
}
