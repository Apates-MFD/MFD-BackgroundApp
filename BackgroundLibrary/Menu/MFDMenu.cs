using BackgroundLibrary.CommandFactory;
using BackgroundLibrary.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace BackgroundLibrary.Menu
{
    /// <summary>
    /// Class of menu
    /// </summary>
    public class MFDMenu
    {
        
        public List<MenuItem> MenuItems { get; set; }
        public MenuInfo MenuInfo { get; set; }
        public MenuDisplay MenuDisplay { get; set; }

        private IPanel panel;
        public SerializableCommand ButtonClick(int position)
        {
            MenuItem item = MenuItems.Find(e => e.Position == position);
            if (item == null) return null;
            return item.Command;
        }

        /// <summary>
        /// get list with properties that have a state
        /// </summary>
        /// <returns></returns>
        public List<string> getLinkedProperties()
        {
            List<string> list = new List<string>();
            MenuItems.ForEach(e =>
            {
                if (e.LinkedProperty != null) list.Add(e.LinkedProperty);
            });
            return list;
        }

        /// <summary>
        /// Updates every button state, if a button got pressed it generates a command.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="state"></param>
        /// <returns>A list of genereted modification commands for UI</returns>
        public List<ModifyPanelCommand> UpdateState(string property, bool state)
        {
            List<ModifyPanelCommand> mods = new List<ModifyPanelCommand>();
            MenuItems.ForEach(e =>
           {
               if (e.LinkedProperty == property)
               {
                   e.State = state;
                   if (panel != null)
                   {
                       mods.Add(new ModifyPanelCommand()
                       {
                           Panel = panel,
                           ChangeType = ModifyPanelChangeType.SETINVERTED,
                           Parameter = state,
                           Position = e.Position
                       });
                   }
               }
           });
            return mods;
        }

        /// <summary>
        /// Sets panel and return a list of commands for panel
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public List<ModifyPanelCommand> SetPanel(IPanel panel)
        {
            this.panel = panel;
            List<ModifyPanelCommand> mods = new List<ModifyPanelCommand>();
            MenuItems.ForEach(e =>
            {
                mods.Add(new ModifyPanelCommand()
                {
                    Panel = panel,
                    ChangeType = ModifyPanelChangeType.SETTEXT,
                    Parameter = e.DisplayText,
                    Position = e.Position
                });
                mods.Add(new ModifyPanelCommand()
                {
                    Panel = panel,
                    ChangeType = ModifyPanelChangeType.SETINVERTED,
                    Parameter = e.DisplayText.Equals(this.MenuInfo.MenuName) ? true : e.State,
                    Position = e.Position
                });
            });
            return mods;
        }

        /// <summary>
        /// Gets assigned pannel
        /// </summary>
        /// <returns></returns>
        public IPanel GetPanel()
        {
            return panel;
        }

        /// <summary>
        /// Serialized current Menu. Only needed for Menu Creator
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="path"></param>
        public static void Serialize(MFDMenu menu, string path)
        {
            File.Delete(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(menu, Formatting.Indented));
        }

        /// <summary>
        /// Deserializes menu from json file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static MFDMenu Deserialize(string path)
        {
            return JsonConvert.DeserializeObject<MFDMenu>(File.ReadAllText(path));
        }
    }
}
