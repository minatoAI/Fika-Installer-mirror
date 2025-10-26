using Fika_Installer.Models;
using Fika_Installer.Models.Spt;
using Fika_Installer.UI.Pages;

namespace Fika_Installer.UI
{
    public class MenuFactory(string installDir)
    {
        private string _fikaCorePath = Path.Combine(installDir, @"BepInEx\plugins\Fika\Fika.Core.dll");
        private string _fikaHeadlessPath = Path.Combine(installDir, @"BepInEx\plugins\Fika\Fika.Headless.dll");

        public Menu CreateMainMenu()
        {
            List<MenuChoice> mainMenuChoices = [];

            bool fikaDetected = File.Exists(_fikaCorePath);

            if (fikaDetected)
            {
                UpdateFikaPage updateFikaPage = new(installDir);

                MenuChoice updateFikaChoice = new("更新Fika模组", updateFikaPage);
                mainMenuChoices.Add(updateFikaChoice);

                UninstallFikaPage uninstallFikaPage = new(this, installDir);

                MenuChoice uninstallFikaChoice = new("卸载Fika", uninstallFikaPage);
                mainMenuChoices.Add(uninstallFikaChoice);
            }
            else
            {
                InstallFikaPage installFikaPage = new(installDir);

                MenuChoice installFikaChoice = new("安装Fika", installFikaPage);
                mainMenuChoices.Add(installFikaChoice);
            }

            MenuChoice advancedChoice = new("更多选项", CreateAdvancedOptionsMenu());
            mainMenuChoices.Add(advancedChoice);

            Menu mainMenu = new(mainMenuChoices);

            return mainMenu;
        }

        public Menu CreateAdvancedOptionsMenu()
        {
            List<MenuChoice> advancedMenuChoices = [];

            bool fikaHeadlessDetected = File.Exists(_fikaHeadlessPath);

            if (fikaHeadlessDetected)
            {
                UpdateFikaHeadlessPage updateFikaHeadlessPage = new(installDir);

                MenuChoice updateFikaHeadlessChoice = new("更新Fika Headless", updateFikaHeadlessPage);
                advancedMenuChoices.Add(updateFikaHeadlessChoice);
            }
            else
            {
                InstallFikaHeadlessPage installFikaHeadlessPage = new(this, installDir);

                MenuChoice installFikaHeadlessChoice = new("安装Fika Headless", installFikaHeadlessPage);
                advancedMenuChoices.Add(installFikaHeadlessChoice);
            }

            bool fikaCoreDetected = File.Exists(_fikaCorePath);

            if (!fikaCoreDetected)
            {
                InstallFikaCurrentDirPage installFikaCurrentDirPage = new(this, installDir);

                MenuChoice installFikaInCurrentFolder = new("在当前文件夹下安装Fika", installFikaCurrentDirPage);
                advancedMenuChoices.Add(installFikaInCurrentFolder);
            }

            MenuChoice backChoice = new("Back", () => { });
            advancedMenuChoices.Add(backChoice);

            Menu advancedOptionsMenu = new(advancedMenuChoices);

            return advancedOptionsMenu;
        }

        public Menu CreateProfileSelectionMenu(List<SptProfile> sptProfiles)
        {
            List<MenuChoice> profileSelectionMenuChoices = [];

            foreach (SptProfile profile in sptProfiles)
            {
                MenuChoice menuChoice = new(profile.ProfileId);
                profileSelectionMenuChoices.Add(menuChoice);
            }

            MenuChoice createNewHeadlessProfile = new("创建一个新的headless profile", "createNewHeadlessProfile");
            profileSelectionMenuChoices.Add(createNewHeadlessProfile);

            Menu profileSelectionMenu = new("选择headless客户端使用的headless profile:", profileSelectionMenuChoices);

            return profileSelectionMenu;
        }

        public Menu CreateInstallMethodMenu()
        {
            List<MenuChoice> choices = [];

            string[] installMethods = Enum.GetNames<InstallMethod>();

            foreach (string installMethod in installMethods)
            {
                MenuChoice choice = new(installMethod);
                choices.Add(choice);
            }

            Menu installMethodMenu = new("选择安装方法，该选择会影响复制SPT文件夹的操作方法。", choices);

            return installMethodMenu;
        }

        public Menu CreateConfirmUninstallFikaMenu()
        {
            List<MenuChoice> choices = [];

            MenuChoice choiceYes = new("Yes");
            choices.Add(choiceYes);

            MenuChoice choiceNo = new("No");
            choices.Add(choiceNo);

            Menu uninstallFikaMenu = new("确认要卸载Fika吗？卸载后Fika的设置将会丢失。", choices);

            return uninstallFikaMenu;
        }
    }
}
