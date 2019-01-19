using System;
using System.IO;
using System.Net;
using System.Text;

namespace web_scraper
{
    class WebRequestGet
    {
        //General Function to request data from a Server
        public static string URLRequest(string url)
        {
            // Prepare the Request
            WebRequest request = WebRequest.Create(url);
            // Set the credentials to default, if required.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Set method to GET to retrieve data
            request.Method = "GET";
            request.Timeout = 6000; //60 second timeout
            ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)";

            string responseContent = null;

            // Get the Response
            using (WebResponse response = request.GetResponse())
            {
                // Retrieve a handle to the Stream
                using (Stream stream = response.GetResponseStream())
                {
                    // Begin reading the Stream
                    using (StreamReader streamreader = new StreamReader(stream))
                    {
                        // Read the Response Stream to the end
                        responseContent = streamreader.ReadToEnd();
                    }
                }
            }

            return (responseContent);
        }
    }
}
