using System;
using System.Collections.Generic;
using System.Net;
using JSONParsing.Model;
using Newtonsoft.Json;

namespace JSONParsing.DataAccessLayer
{
    public class DataExtractor
    {
        public List<string> DownloadedUrls;
        private WebClient _webClient;

        public DataExtractor()
        {
            DownloadedUrls = new List<string>{ };
        }

        public List<string> Extract(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                    throw new Exception("url shouldn't be null or empty");

                _webClient = new WebClient();
                string downloadedString =  _webClient.DownloadString(new Uri(url));
                var root =  JsonConvert.DeserializeObject<List<ImageModel>>(downloadedString);

                if (root != null && root.Count > 0)
                {
                    foreach (ImageModel imageModel in root)
                    {
                        DownloadedUrls.Add(imageModel.download_url);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception thrown" + ex.Message);
                throw ex;
            }

            return DownloadedUrls;
        }
    }
}
