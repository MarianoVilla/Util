using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Rama
{
    class Tester
    {
        public void Search(string bandName)
        {
            string searchQuery = bandName + "Discography";
            bool searchResultType;
            string CX = "003329525402407492957:4labmtz-bns";
            string APIKey = "AIzaSyCwNmR9FZg8_sjCH7G5ca6eMgQG6-JNdu0";
            //WebRequest request = WebRequest.Create("https://www.googleapis.com/customsearch/v1?key=" + APIKey + "&cx=" + CX + "&q=" + searchQuery);
            bandName = bandName.Replace(" ","+");
            bandName += "+discography";
            WebRequest request = WebRequest.Create("https://www.google.com/search?safe=off&ei=6c1gXJeRLpPE5OUPvOW0gAE&q="+bandName.ToLower()+"&oq=dream&gs_l=psy-ab.3.0.35i39l2j0i67l8.17295.17886..19319...1.0..0.61.296.5......0....1..gws-wiz.....6..0j0i131j0i20i263.MgwlhQDKv70");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var responseString = reader.ReadToEnd();
            if(responseString.Contains("g-scrolling-carousel")){
                searchResultType = true;
            }
        }
    }
}
