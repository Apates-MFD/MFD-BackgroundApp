using System;

namespace EDLibrary.CommandFactory
{
    public class ModifyPanelCommand : Command
    {
        public UI.IPanel Panel { get; set; }
        public ModifyPanelChangeType ChangeType { get; set; }
        public int Position { get; set; }
        public dynamic Parameter { get; set; }
        public override void Execute(object sender)
        {
            if (Panel == null || Parameter == null || ChangeType == ModifyPanelChangeType.NULL)
            {
                throw new ArgumentException("Parameter not set");
            }

            if (Parameter == null && ChangeType != ModifyPanelChangeType.CLEAR) throw new ArgumentException("Parameter Cannot be null");

            switch (ChangeType)
            {
                case ModifyPanelChangeType.SETTEXT:
                    Panel.SetText(Position, Parameter);
                    break;
                case ModifyPanelChangeType.SETENABLED:
                    Panel.SetEnabled(Position, Parameter);
                    break;
                case ModifyPanelChangeType.SETINVERTED:
                    Panel.SetInverted(Position, Parameter);
                    break;
                case ModifyPanelChangeType.CLEAR:
                    Panel.Clear();
                    break;
                case ModifyPanelChangeType.NULL:
                    throw new ArgumentException("Modify Type cannot be null");
            }
        }
    }


    public enum ModifyPanelChangeType
    {
        NULL,
        SETTEXT,
        SETENABLED,
        SETINVERTED,
        CLEAR
    }
}
