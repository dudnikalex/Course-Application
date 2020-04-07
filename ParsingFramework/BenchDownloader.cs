using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ParsingFramework
{
    public class BenchDownloader
    {
        private string path;
        private string[] data;
        private WebClient wc;

        public BenchDownloader()
        {
            wc = new WebClient();
            path = @"../../Files/";
        }

        public void Download(string url, string name)
        {
            wc.DownloadFile(url, path + name);
        }
    }
}
