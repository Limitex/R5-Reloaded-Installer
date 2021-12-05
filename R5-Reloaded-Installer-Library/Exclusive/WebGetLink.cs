using R5_Reloaded_Installer_Library.Get;
using R5_Reloaded_Installer_Library.IO;
using R5_Reloaded_Installer_Library.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R5_Reloaded_Installer_Library.Exclusive
{
    public static class WebGetLink
    {
        public static string DetoursR5()
        {
            foreach (var link in GitHub.GetLatestRelease("Mauler125", "detours_r5"))
                if (StringProcessing.GetExtension(link) == "zip")
                    return link;
            throw new("Unable to retrieve the link for detours_r5.");
        }

        public static string ScriptsR5() => 
            "https://github.com/Mauler125/scripts_r5/archive/refs/heads/S3_N1094.zip";

        public static string ApexClient() =>
            "https://cdn.discordapp.com/attachments/876546839304888362/877972293844893786/R5pc_r5launch_N1094_CL456479_2019_10_30_05_20_PM.torrent";

        public static string WorldsEdgeAfterDark() =>
            "https://eax.re/r5/mp_rr_desertlands_64k_x_64k_nx.7z";
    }
}
