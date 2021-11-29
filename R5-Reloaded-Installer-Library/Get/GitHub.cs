using Newtonsoft.Json.Linq;
using R5_Reloaded_Installer_Library.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace R5_Reloaded_Installer_Library.Get
{
    public static class GitHub
    {
        public static string[] GetLatestRelease(string owner, string repo)
        {
            var api = PathExpansion.Combine("https://api.github.com/", "repos", owner, repo, "releases", "latest");
            var request = new HttpRequestMessage(HttpMethod.Get, api);
            request.Headers.Add("User-Agent", "0");
            var result = new HttpClient().SendAsync(request).Result.Content.ReadAsStringAsync(); result.Wait();
            var json = JObject.Parse(result.Result);
            var links = new List<string>();
            foreach (var link in json["assets"] ?? string.Empty)
                links.Add((string?)link["browser_download_url"] ?? string.Empty);
            links.RemoveAll(item => item == string.Empty);
            links.Sort();
            return links.ToArray();
        }
    }
}
