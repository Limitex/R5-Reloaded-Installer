﻿using R5_Reloaded_Installer_Library.Exclusive;
using R5_Reloaded_Installer_Library.Get;
using R5_Reloaded_Installer_Library.IO;
using R5_Reloaded_Installer_Library.Text;

[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]

var FinalDirectoryName = "R5-Reloaded";
var ScriptsDirectoryPath = Path.Combine("platform", "scripts");
var WorldsEdgeAfterDarkPath = "package";
var DirectionPath = Path.GetDirectoryName(Environment.ProcessPath);
var AllAboutByteSize = 42f * 1024f * 1024f * 1024f;

if (DirectionPath == null) throw new Exception();
DirectionPath = Path.Combine(DirectionPath, FinalDirectoryName);

ConsoleExpansion.DisableEasyEditMode();
Console.WriteLine("\n" +
    "  -----------------------------------\n" +
    "  ||                               ||\n" +
    "  ||     R5-Reloaded Installer     ||\n" +
    "  ||                               ||\n" +
    "  -----------------------------------\n\n" +
    "  This program was created by Limitex.\n" +
    "  Please refer to the link below for the latest version of this program.\n\n" +
    "  https://github.com/Limitex/R5-Reloaded-Installer/releases \n\n" +
    "Welcome!\n");

if (!(InstalledApps.DisplayNameList() ?? new string[0]).Contains("Origin"))
{
    ConsoleExpansion.LogError("\'Origin\' is not installed.");
    ConsoleExpansion.LogError("Do you want to continue?");
    ConsoleExpansion.LogError("R5 Reloaded cannot be run without \'Origin\' installed.");
    if (!ConsoleExpansion.ConsentInput()) ConsoleExpansion.Exit();
}

ConsoleExpansion.LogWrite("Get the download location...");
var detoursR5_link = WebGetLink.DetoursR5();
var scriptsR5_link = WebGetLink.ScriptsR5();
var apexClient_link = WebGetLink.ApexClient();
var worldsEdgeAfterDark_link = WebGetLink.WorldsEdgeAfterDark();

ConsoleExpansion.LogWrite("Get the file size...");
var DriveRoot = Path.GetPathRoot(DirectionPath) ?? string.Empty;
var DriveSize = FileExpansion.GetDriveFreeSpace(DriveRoot);
ConsoleExpansion.LogWrite("[" + DriveRoot + "] Drive Size   >> " + StringProcessing.ByteToStringWithUnits(DriveSize));
ConsoleExpansion.LogWrite("Download File Size >> " + StringProcessing.ByteToStringWithUnits(AllAboutByteSize));
if (AllAboutByteSize > DriveSize)
{
    ConsoleExpansion.LogError("There is not enough space on the destination drive to install the software.");
    ConsoleExpansion.LogWrite("Do you want to continue?");
    if (!ConsoleExpansion.ConsentInput()) ConsoleExpansion.Exit();
}
ConsoleExpansion.LogWrite("(OK)");

ConsoleExpansion.LogNotes("It will take about 30 minutes, depending on the Seeder situation.");
ConsoleExpansion.LogNotes("Do not delete the directory generated by this program.");
ConsoleExpansion.LogWrite("Do you want to continue the installation ?");
if (!ConsoleExpansion.ConsentInput()) ConsoleExpansion.Exit();

ConsoleExpansion.LogWrite("Preparing...");
using (var download = new Download(DirectionPath))
{
    download.ProcessReceives += (appType, outline) => ConsoleExpansion.LogWrite("[ " + appType + " ] : " + outline);
    ConsoleExpansion.LogWrite("The download will start.");
    ConsoleExpansion.WriteWidth('=', "Downloading Worlds edge after dark");
    var worldsEdgeAfterDarkDirPath = download.Run(WebGetLink.WorldsEdgeAfterDark(), "WorldsEdgeAfterDark");
    ConsoleExpansion.WriteWidth('=', "Downloading detours_r5");
    var detoursR5DirPath = download.Run(WebGetLink.DetoursR5(), "detoursR5");
    ConsoleExpansion.WriteWidth('=', "Downloading scripts r5");
    var scriptsR5DirPath = download.Run(WebGetLink.ScriptsR5(), "scriptsR5");
    ConsoleExpansion.WriteWidth('=', "Downloading Apex Client Season3");
    var apexClientDirPath = download.Run(WebGetLink.ApexClient(), FinalDirectoryName);
    ConsoleExpansion.WriteWidth('=');
    ConsoleExpansion.LogWrite("Creating the R5-Reloaded");
    DirectoryExpansion.MoveOverwrite(detoursR5DirPath, apexClientDirPath);
    Directory.Move(scriptsR5DirPath, Path.Combine(apexClientDirPath, ScriptsDirectoryPath));
    DirectoryExpansion.MoveOverwrite(Path.Combine(worldsEdgeAfterDarkDirPath, WorldsEdgeAfterDarkPath), apexClientDirPath);
    DirectoryExpansion.DirectoryDelete(worldsEdgeAfterDarkDirPath);
    ConsoleExpansion.LogWrite("Done.");
}
ConsoleExpansion.Exit();