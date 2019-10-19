//using System;
//using System.IO;
//using System.Net;
//using System.Text;
//using System.Web;

//namespace Xplorium.Core.Utilities.Loggers
//{
//    public class WebsiteLogger : Logger
//    {
//        public string Url { get; set; }
//        public string QueryString { get; set; }

//        public override void LogError(string error)
//        {

//            Uri uri = new Uri(Url);
//            var httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
//            httpWebRequest.Method = "POST";
//            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

//            Encoding encoding = Encoding.Default;

//            string parameters = string.Format(QueryString, HttpUtility.UrlEncode(error));

//            var memStream = new MemoryStream();
//            var streamWriter = new StreamWriter(memStream, encoding);
//            streamWriter.Write(parameters);
//            streamWriter.Flush();
//            httpWebRequest.ContentLength = memStream.Length;
//            streamWriter.Close();

//            var stream = httpWebRequest.GetRequestStream();
//            streamWriter = new StreamWriter(stream, encoding);
//            streamWriter.Write(parameters);
//            streamWriter.Close();

//            using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
//            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
//            {
//                streamReader.ReadToEnd();
//            }
//        }
//    }
//}

