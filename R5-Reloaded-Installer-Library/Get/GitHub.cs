using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace R5_Reloaded_Installer_Library.Get
{
    public static class GitHub
    {
        public static string[] GetLatestRelease(string owner, string repo)
        {
            var api = Combine("https://api.github.com/", "repos", owner, repo, "releases", "latest");
            var links = new List<string>();
            var request = WebRequest.Create(api); request.Headers.Add("User-Agent", "0");
            using var reader = new StreamReader(request.GetResponse().GetResponseStream());
            var json = JObject.Parse(reader.ReadToEnd());
            foreach (var link in json["assets"]) links.Add((string)link["browser_download_url"]);
            return links.ToArray();
        }

        private static string Combine(params string[] paths)
        {
            string path = string.Empty;
            foreach (var str in paths) path = Path.Combine(path, str);
            return path.Replace(@"\", "/");
        }
    }
}
