﻿using EDLibrary.Configuration;
using EDLibrary.ControllInput;
using EDLibrary.Menu;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
namespace EDLibrary.Handlers
{
    /// <summary>
    /// Configuration Handler
    /// <para> Handles config and menu files</para>
    /// </summary>
    class ConfigurationHandler
    {
        private Config config;
        private List<MFDMenu> availableMenus = null;
        private MFDMenu mainMenu = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public ConfigurationHandler()
        {
            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
            List<InputDeviceNames> devices = new List<InputDeviceNames>(InputDeviceNames.GetAll());
            config.InputDevices = new List<InputDeviceNames>();
            config.InputDevicesStrings.ForEach(s =>
               {
                   devices.ForEach(d =>
                   {
                       if (d.Value == s) config.InputDevices.Add(d);
                   });
               });
        }

        /// <summary>
        /// Serializes current config object
        /// </summary>
        private void saveConfig()
        {
            File.WriteAllText("config.json",JsonConvert.SerializeObject(config, Formatting.Indented));
        }

        /// <summary>
        /// Writer window position into config
        /// </summary>
        /// <param name="name"></param>
        /// <param name="property"></param>
        public void SaveWindowPosition(InputDeviceNames name, double[] property)
        {
            if (config.WindowProperties == null) config.WindowProperties = new Dictionary<string, double[]>();
            if (config.WindowProperties.ContainsKey(name.Value))
            {
                config.WindowProperties[name.Value] = property;
            }
            else
            {
                config.WindowProperties.Add(name.Value, property);
            }

            saveConfig();
        }

        /// <summary>
        /// Gets window position from config
        /// </summary>
        /// <param name="name"></param>
        /// <param name="property"></param>
        public double[] GetWindowPosition(InputDeviceNames name)
        {
            if (config.WindowProperties == null || !config.WindowProperties.ContainsKey(name.Value)) return null;
            return config.WindowProperties[name.Value];
        }
        /// <summary>
        /// Reads all menus from menu path
        /// </summary>
        /// <returns>List of menus</returns>
        /// <exception cref="Exception">When Path is invalid or no menus got loaded</exception>
        private void loadMenus()
        {
            if (!Directory.Exists(config.PathToMenuFolder)) throw new Exception("Menu folder not found");

            List<MFDMenu> menus = new List<MFDMenu>();
            string[] menuPaths = Directory.GetFiles(config.PathToMenuFolder);
            foreach (string menupath in menuPaths)
            {
                menus.Add(MFDMenu.Deserialize(menupath));
            }

            if(menus.Count == 0) throw new Exception("Could not load any menu");
            availableMenus = menus;
        }

        /// <summary>
        /// Searches main menu
        /// </summary>
        /// <returns></returns>
        public MFDMenu GetMainMenu()
        {
            if (mainMenu == null) mainMenu = GetMenu(config.MainMenuName);

            return mainMenu;
        }

        /// <summary>
        /// Searches menu with given name
        /// </summary>
        /// <param name="MenuName"></param>
        /// <returns></returns>
        public MFDMenu GetMenu(string MenuName)
        {
            if (availableMenus == null)
            {
                try
                {
                    loadMenus();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            MFDMenu menu = availableMenus.Find(e => e.MenuInfo.MenuName == MenuName);
            if (menu == null) throw new Exception("Menu not found");
            return menu;
        }
        /// <summary>
        /// Gives devices defined from config 
        /// </summary>
        /// <returns></returns>
        public List<InputDeviceNames> GetInputDevices()
        {
            return config.InputDevices;
        }

        /// <summary>
        /// Return setting on which button state (Press or Release) an action gets triggered.
        /// </summary>
        /// <returns></returns>
        public bool GetTriggerState()
        {
            return config.ButtonTriggerOnPress;
        }

        /// <summary>
        /// returns path
        /// </summary>
        /// <returns></returns>
        public string GetPathToStatusFolder()
        {
            return config.PathToStatusFolder;
        }

        /// <summary>
        /// returns path
        /// </summary>
        /// <returns></returns>
        public string GetPathToKeybindings()
        {
            return config.PathToConfiguration+"keybindings.json";
        }
    }
}
