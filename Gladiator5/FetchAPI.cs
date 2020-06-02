using System;
using System.Net;
using System.IO;
using static System.Web.Helpers;

namespace Gladiator5.Desktop
{

    public class FetchAPI
    {


        public static string get(string url)
        {
            string urlapi = String.Format(url);
            WebRequest request = WebRequest.Create(urlapi);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIyMjBmNzg1OGYzNDQ3MjY5ZjcxYzQyODIyMzE2MjVhMyIsInN1YiI6IjVlZDRjNzEyNTI4YjJlMDAxZTZiYTRkZCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.R3TIZwcuN_46iZfFfdnkK4tdLaPuLfkTQUkpI74pzIE");
            HttpWebResponse response = null;
            response = (HttpWebResponse)request.GetResponse();

            string json = null;
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                json = sr.ReadToEnd();
                sr.Close();
            }
            return json;
        }
        public FetchAPI()
        {

        }
    }
}
