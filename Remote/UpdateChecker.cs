using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Cache;

namespace GRemote
{
    public class UpdateChecker
    {
        WebClient client;

        public UpdateChecker()
        {
            WebRequest.DefaultWebProxy = null;
            WebRequest.DefaultCachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            client = new WebClient();
        }
    }
}
