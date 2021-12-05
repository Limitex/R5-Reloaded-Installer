using R5_Reloaded_Installer_Library.Exclusive;
using R5_Reloaded_Installer_Library.Get;
using R5_Reloaded_Installer_Library.IO;

var FinalDirectoryName = "R5-Reloaded";
var ScriptsDirectoryPath = Path.Combine("platform", "scripts");
var WorldsEdgeAfterDarkPath = "package";
var DirectionPath = Path.GetDirectoryName(Environment.ProcessPath);

if (DirectionPath == null) throw new Exception();

using (var download = new Download(DirectionPath))
{
    download.ProcessReceives += (appType, outline) => ConsoleExpansion.LogWrite("[ " + appType + " ] : " + outline);
    var worldsEdgeAfterDarkDirPath = download.Run(WebGetLink.WorldsEdgeAfterDark(), "WorldsEdgeAfterDark");
    var detoursR5DirPath = download.Run(WebGetLink.DetoursR5(), "detoursR5");
    var scriptsR5DirPath = download.Run(WebGetLink.ScriptsR5(), "scriptsR5");
    var apexClientDirPath = download.Run(WebGetLink.ApexClient(), FinalDirectoryName);

    DirectoryExpansion.MoveOverwrite(detoursR5DirPath, apexClientDirPath);
    Directory.Move(scriptsR5DirPath, Path.Combine(apexClientDirPath, ScriptsDirectoryPath));
    DirectoryExpansion.MoveOverwrite(Path.Combine(worldsEdgeAfterDarkDirPath, WorldsEdgeAfterDarkPath), apexClientDirPath);
    DirectoryExpansion.DirectoryDelete(worldsEdgeAfterDarkDirPath);
    ConsoleExpansion.LogWrite("Done.");
}


