using Esri.ArcGISRuntime.Portal;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TokenAuthentication.Example.Core;
using TokenAuthentication.Example.Events;

namespace TokenAuthentication.Example.ViewModels
{
    public class WebMapsViewModel : BaseViewModel
    {
        public WebMapsViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            EventAggregator.GetEvent<UserSessionCreatedEvent>().Subscribe(async args => {
                await LoadItemsAsync(args.Portal);
            });
        }

        private ObservableCollection<PortalItem> _webMaps = new ObservableCollection<PortalItem>();
        public ObservableCollection<PortalItem> WebMaps
        {
            get { return _webMaps; }
            set { SetProperty(ref _webMaps, value); }
        }

        private async Task LoadItemsAsync(ArcGISPortal portal)
        {
            var results = await portal.FindItemsAsync(new PortalQueryParameters(@"type:'web map' AND  NOT(type:'Web Mapping Application')")
            {
                CanSearchPublic = false,  // Find only items from used portal
                Limit = 20,
                SortField = "relevance"
            }); 
            foreach (var item in results.Results)
            {
                WebMaps.Add(item);
            }
        }
    }
}
