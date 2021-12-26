using R5_Reloaded_Installer_Library.Get;
using R5_Reloaded_Installer_Library.IO;

namespace R5_Reloaded_Installer_Library.Exclusive
{
    public static class WebGetLink
    {
        public static string DetoursR5()
        {
            var links = GitHub.GetLatestRelease("Mauler125", "r5apexsdk");
            foreach (var link in links)
                if (FileExpansion.GetExtension(link) == "zip")
                    return link;
            ConsoleExpansion.Exit();
            return null;
        }

        public static string ScriptsR5()
        {
            return "https://github.com/Mauler125/scripts_r5/archive/refs/heads/S3_N1094.zip";
        }

        public static string ApexClient()
        {
            return "https://cdn.discordapp.com/attachments/876546839304888362/877972293844893786/R5pc_r5launch_N1094_CL456479_2019_10_30_05_20_PM.torrent";
        }

        public static string WorldsEdgeAfterDark()
        {
            return "https://eax.re/r5/mp_rr_desertlands_64k_x_64k_nx.7z";
        }
    }
}
