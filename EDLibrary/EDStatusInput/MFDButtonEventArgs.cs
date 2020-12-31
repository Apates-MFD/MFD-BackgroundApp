using System;

namespace EDLibrary.EDStatusInput
{
    public class MFDButtonEventArgs : EventArgs
    {
        public MFDButton Button { get; set; }
    }
    public class MFDMenuButtonEventArgs : EventArgs
    {
        public MenuButtonTypes ButtonType { get; set; }
    }

    public enum MenuButtonTypes
    {      
        BRTP =24,
        BRTM = 25,
        SYMP =20,
        SYMM = 21,
        CONP =22,
        CONM = 23
    }

}
