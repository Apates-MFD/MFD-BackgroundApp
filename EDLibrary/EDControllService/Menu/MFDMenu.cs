using EDLibrary.EDControllService.CommandFactory;
using EDLibrary.UI;
using System;
using System.Collections.Generic;

namespace EDLibrary.EDControllService.Menu
{
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

        public List<string> getLinkedProperties()
        {
            List<string> list = new List<string>();
            MenuItems.ForEach(e =>
            {
                if (e.LinkedProperty != null) list.Add(e.LinkedProperty);
            });
            return list;
        }

        public void UpdateState(string property, bool state)
        {
            MenuItems.ForEach(e =>
           {
               if (e.LinkedProperty == property)
               {
                   e.State = state;
                   if (panel != null)
                   {
                       new ModifyPanelCommand() { 
                           Panel = panel,
                           ChangeType = ModifyPanelChangeType.SETINVERTED, 
                           Parameter = state, Position = e.Position 
                       }.Execute();
                   }
               }
           });
        }

        public void SetPanel(IPanel panel)
        {
            this.panel = panel;
            MenuItems.ForEach(e => {
                new ModifyPanelCommand()
                {
                    Panel = panel,
                    ChangeType = ModifyPanelChangeType.SETTEXT,
                    Parameter = e.DisplayText,
                    Position = e.Position
                }.Execute();
                new ModifyPanelCommand()
                {
                    Panel = panel,
                    ChangeType = ModifyPanelChangeType.SETINVERTED,
                    Parameter = e.DisplayText.Equals(this.MenuInfo.MenuText) ? true : e.State,
                    Position = e.Position
                }.Execute();
            });        
        }
        public IPanel GetPanel()
        {
            return panel;
        }
    }
}
