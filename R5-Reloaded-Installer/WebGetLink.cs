using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace R5_Reloaded_Installer
{
    static class WebGetLink
    {
        private static string AssetsClassElementSelector = "div.Box--condensed a";

        private static string[] GetGitHubLatestRelease(string username, string repository)
        {
            var targeturl = "https://github.com/" + username + "/" + repository + "/releases/latest/";

            var htmlDocument = new HtmlParser().ParseDocument(new WebClient().DownloadString(targeturl));
            var urlElements = htmlDocument.QuerySelectorAll(AssetsClassElementSelector);

            var downloadLinks = new List<string>();
            foreach (var urlElement in urlElements)
            {
                var url = urlElement.GetAttribute("href");
                if (!url.Contains("archive")) downloadLinks.Add("https://github.com" + url);
            }
            return downloadLinks.ToArray();
        }

        public static string GetDetoursR5Link()
        {
            var links = GetGitHubLatestRelease("Mauler125", "detours_r5");
            foreach (var link in links)
                if (Path.GetExtension(Path.GetFileName(link)).Replace(".", "") == "zip")
                    return link;
            ConsoleExpansion.LogWriteLineError("The aria2 link was not found.");
            ConsoleExpansion.LogWriteLineError("Please contact the developer.");
            ConsoleExpansion.ExitConsole();
            return null;
        }
        
        public static string GetAria2Link()
        {
            var links = GetGitHubLatestRelease("aria2", "aria2");
            foreach (var link in links) 
                if (Path.GetFileName(link).Contains("win-64bit")) 
                    return link;
            ConsoleExpansion.LogWriteLineError("The aria2 link was not found.");
            ConsoleExpansion.LogWriteLineError("Please contact the developer.");
            ConsoleExpansion.ExitConsole();
            return null;
        }
    }
}
