using System.Collections.Generic;
using System.IO;
using System.Net;
using AngleSharp.Html.Parser;

namespace R5_Reloaded_Installer
{
    static class WebGetLink
    {
        private static string[] GetGitHubLatestRelease(string username, string repository)
        {
            var targeturl = "https://github.com/" + username + "/" + repository + "/releases/latest/";

            var htmlDocument = new HtmlParser().ParseDocument(new WebClient().DownloadString(targeturl));
            var urlElements = htmlDocument.QuerySelectorAll("div.Box--condensed a");

            var downloadLinks = new List<string>();
            foreach (var urlElement in urlElements)
            {
                var url = urlElement.GetAttribute("href");
                if (!url.Contains("archive")) downloadLinks.Add("https://github.com" + url);
            }
            return downloadLinks.ToArray();
        }

        public static string GetAria2Link()
        {
            var links = GetGitHubLatestRelease("aria2", "aria2");
            foreach (var link in links)
                if (Path.GetFileName(link).Contains("win-64bit")) 
                    return link;
            ConsoleExpansion.LogError("The aria2 link was not found.");
            ConsoleExpansion.LogError("Please contact the developer.");
            ConsoleExpansion.Exit();
            return null;
        }

        public static string GetDetoursR5Link()
        {
            ConsoleExpansion.LogWrite("Getting a download link for detours_r5.");
            var links = GetGitHubLatestRelease("Mauler125", "detours_r5");
            foreach (var link in links)
            {
                if (Path.GetExtension(Path.GetFileName(link)).Replace(".", "") == "zip")
                {
                    ConsoleExpansion.LogWrite("Success.");
                    return link;
                }
            }
            ConsoleExpansion.LogError("The aria2 link was not found.");
            ConsoleExpansion.LogError("Please contact the developer.");
            ConsoleExpansion.Exit();
            return null;
        }

        public static string GetScriptsR5Link()
        {
            ConsoleExpansion.LogWrite("Setting a download link for scripts_r5");
            return "https://github.com/Mauler125/scripts_r5/archive/refs/heads/S3_N1094.zip";
        }

        public static string GetApexClientLink()
        {
            ConsoleExpansion.LogWrite("Setting a download link for ApexClient");
            return "https://cdn.discordapp.com/attachments/872057782071869502/877987963303260201/R5pc_r5launch_N1094_CL456479_2019_10_30_05_20_PM.torrent";
        }
    }
}
