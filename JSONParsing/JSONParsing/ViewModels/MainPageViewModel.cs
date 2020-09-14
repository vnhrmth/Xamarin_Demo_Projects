using System.Collections.Generic;
using JSONParsing.DataAccessLayer;
using MvvmHelpers;

namespace JSONParsing.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private List<string> _downloadedUrls;
        public List<string> DownloadedUrls
        {
            get
            {
                return _downloadedUrls;
            }
            set
            {
                SetProperty(ref _downloadedUrls, value, nameof(DownloadedUrls));
            }
        }

        public readonly string Url = "https://picsum.photos/v2/list";

        public MainPageViewModel()
        {
            _downloadedUrls = new List<string>();

            DataExtractor dataExtractor = new DataExtractor();
            DownloadedUrls = dataExtractor.Extract(Url);
        }
    }
}
