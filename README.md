# [ important ] Migrate to R5-Reloaded-Downloader (pseudonym).　

I created a new project because I no longer need to download torrents and I don't need to use the external application I was building in.
This is safer and more accurate than R5-Reloaded-Installer (now).

It's still Pre-Release, so it's named Downloader, but after release, this repository will be renamed to "R5-Reloaded-installer-with-Torrent" and R5-Reloaded-Downloader will be renamed to R5-Reloaded-installer.

↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

https://github.com/Limitex/R5-Reloaded-Downloader

↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

# R5-Reloaded-Installer

This is the installer for R5 Reloaded.

It is an installer that even installs detours_r5, scripts_r5 and WordsEdgeAfterDark on the game client.

You can select the software to be used when downloading.

You can choose either Aria2 or Transmission to download the game client. For other downloads, you can select Aria2 or HttpClient.

If you are unable to download the software selected by default, please select another software.

# About Transmission
Transmisson's original software doesn't officially build cli, so we're using a fork of the wrapper project.
Therefore, there are dependencies.

Source >> [transmission/transmisson](https://github.com/transmission/transmission)

Visual studio wrapper >> [depler/transmission-vs](https://github.com/depler/transmission-vs)

Dependent Project >> [Limitex/transmission-vs](https://github.com/Limitex/transmission-vs)

# About Aria2

Aria2 is a download manager that is commonly used in Linux and other applications.

In this software, we are using the Windows CLI version.

# About HttpClient

Use and download HttpClient, which is a C# class library.

# How to use

There is a GUI and CLI in the release.

The GUI can be installed like a Windows installer.

CLI runs CLI-based and produces only R5R.

1. Download and unzip the Zip file from the [release](https://github.com/Limitex/R5-Reloaded-Installer/releases).
2. Click R5-Reloaded-Installer.exe to launch it.
3. You will be asked for firewall permission immediately, so please allow it.
4. All you have to do is wait. R5 is stored in the R5-Reloaded directory in the directory.
