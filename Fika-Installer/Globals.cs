﻿using Fika_Installer.Models.Fika;
using System.Reflection;

namespace Fika_Installer
{
    public static class Installer
    {
        private static readonly string _versionMajor = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
        private static readonly string _versionMinor = Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
        private static readonly string _versionBuild = Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();

        public static readonly string VersionString = $"Fika Installer v{_versionMajor}.{_versionMinor}.{_versionBuild}";
        public static readonly string CurrentDir = Directory.GetCurrentDirectory();
        public static readonly string TempDir = Path.Combine(CurrentDir, "FikaInstallerTemp");

        public static string FikaCorePath(string installDir)
        {
            return Path.Combine(installDir, @"BepInEx\plugins\Fika\Fika.Core.dll");
        }
        public static string FikaHeadlessPath(string installDir)
        {
            return Path.Combine(installDir, @"BepInEx\plugins\Fika\Fika.Headless.dll");
        }
    }

    public static class FikaReleaseLists
    {
        private static readonly FikaRelease _fikaCoreRelease = new("Fika.Release", "https://gh-proxy.com/https://api.github.com/repos/project-fika/Fika-Plugin/releases/latest");
        private static readonly FikaRelease _fikaHeadlessRelease = new("Fika.Headless", "https://gh-proxy.com/https://api.github.com/repos/project-fika/Fika-Headless/releases/latest");
        private static readonly FikaRelease _fikaServerRelease = new("Fika.Server", "https://gh-proxy.com/https://api.github.com/repos/project-fika/Fika-Server-CSharp/releases/latest");
        private static readonly FikaRelease _fikaHeadlessManagerRelease = new("Fika.Headless.Manager", "https://gh-proxy.com/https://api.github.com/repos/project-fika/Fika-Headless-Manager/releases/latest");

        public static readonly List<FikaRelease> StandardFika =
        [
            _fikaCoreRelease,
            _fikaServerRelease
        ];

        public static readonly List<FikaRelease> HeadlessFika =
        [
            _fikaCoreRelease,
            _fikaHeadlessRelease,
            _fikaHeadlessManagerRelease
        ];
    }

    public static class SptConstants
    {
        public static readonly string ServerExeName = "SPT.Server.exe";
        public static readonly string LauncherExeName = "SPT.Launcher.exe";
    }

    public static class EftConstants
    {
        public static readonly string GameExeName = "EscapeFromTarkov.exe";
    }
}