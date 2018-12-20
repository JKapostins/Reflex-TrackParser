﻿using System;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace TrackManagement
{
    public abstract class HtmlParser
    {
        public abstract Track[] ParseTracks();

        protected virtual string GetHtml(string url)
        {
            string html = string.Empty;
            using (WebClient client = new WebClient())
            {
                html = client.DownloadString(url);
            }
            return html;
        }

        protected virtual bool FileExistsOnServer(string url)
        {
            bool fileExists = false;
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                fileExists = true;
            }
            catch (WebException)
            {
                //nop since fileExists is false by default
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return fileExists;
        }
    }
}
