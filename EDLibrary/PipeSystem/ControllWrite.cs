using EDLibrary.Menu;
using EDLibrary.Output;
using System;
using System.Collections.Generic;

namespace EDLibrary.PipeSystem
{
    /// <summary>
    /// Converts given action into keystrokes
    /// </summary>
    class ControllWrite : PipeWrite
    {
        private static List<Keybinding> keybindings = null;

        /// <summary>
        /// No exit needed, no threads running
        /// </summary>
        public override void Exit() { }

        /// <summary>
        /// <seealso cref="ControllWrite"/>
        /// </summary>
        /// <param name="data"></param>
        public override void Write(object data)
        {
            if (!data.GetType().Equals(typeof(Actions))) throw new ArgumentException("Argument is not an Action");
            Actions action = (Actions)data;

            if (keybindings == null) keybindings = KeybindingParser.Parse();
            Keybinding binding = keybindings.Find(e => e.Action.Equals(action));

            if (binding == null) throw new Exception("Action not bound to key");

            Keyboard.exec(binding.KeyStrokes);
        }
    }
}
