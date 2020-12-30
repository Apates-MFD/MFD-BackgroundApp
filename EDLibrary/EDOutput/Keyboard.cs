using EDLibrary.EDControllService.Menu;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EDLibrary.EDOutput
{
    /// <summary>
    /// My own question as reference: https://stackoverflow.com/questions/35138778/sending-keys-to-a-directx-game
    /// http://www.gamespp.com/directx/directInputKeyboardScanCodes.html
    /// </summary>
    public class Keyboard
    {
        private static List<Keybinding> keybindings = null;
     
        public static void Execute(Actions action)
        {
            if (keybindings == null) keybindings = KeybindingParser.Parse();
            Keybinding binding = keybindings.Find(e => e.Action.Equals(action));
            if (binding == null) throw new Exception("Action not bound to key!!!+111¨1!+1w");
            exec(binding.KeyStrokes);
        }
        private static void exec(DirectXKeyStrokes[] strokes)
        {
            //Press
            for (int i = 0; i < strokes.Length; i++)
            {
                Keyboard.SendKey(strokes[i], false, Keyboard.InputType.Keyboard);
            }
            //Release
            for (int i = strokes.Length - 1; i >= 0; i--)
            {
                Keyboard.SendKey(strokes[i], true, Keyboard.InputType.Keyboard);
            }
        }

        [Flags]
        public enum InputType
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        [Flags]
        public enum KeyEventF
        {
            KeyDown = 0x0000,
            ExtendedKey = 0x0001,
            KeyUp = 0x0002,
            Unicode = 0x0004,
            Scancode = 0x0008,
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();



        /// <summary>
        /// Sends a directx key.
        /// http://www.gamespp.com/directx/directInputKeyboardScanCodes.html
        /// </summary>
        /// <param name="key"></param>
        /// <param name="KeyUp"></param>
        /// <param name="inputType"></param>
        public static void SendKey(DirectXKeyStrokes key, bool KeyUp, InputType inputType)
        {
            uint flagtosend;
            if (KeyUp)
            {
                flagtosend = (uint)(KeyEventF.KeyUp | KeyEventF.Scancode);
            }
            else
            {
                flagtosend = (uint)(KeyEventF.KeyDown | KeyEventF.Scancode);
            }

            Input[] inputs =
            {
            new Input
            {
                type = (int) inputType,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = 0,
                        wScan = (ushort) key,
                        dwFlags = flagtosend,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            }
        };

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        /// <summary>
        /// Sends a directx key.
        /// http://www.gamespp.com/directx/directInputKeyboardScanCodes.html
        /// </summary>
        /// <param name="key"></param>
        /// <param name="KeyUp"></param>
        /// <param name="inputType"></param>
        public static void SendKey(ushort key, bool KeyUp, InputType inputType)
        {
            uint flagtosend;
            if (KeyUp)
            {
                flagtosend = (uint)(KeyEventF.KeyUp | KeyEventF.Scancode);
            }
            else
            {
                flagtosend = (uint)(KeyEventF.KeyDown | KeyEventF.Scancode);
            }

            Input[] inputs =
            {
            new Input
            {
                type = (int) inputType,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = 0,
                        wScan = key,
                        dwFlags = flagtosend,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            }
        };

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        public struct Input
        {
            public int type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)] public readonly MouseInput mi;
            [FieldOffset(0)] public KeyboardInput ki;
            [FieldOffset(0)] public readonly HardwareInput hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput
        {
            public readonly int dx;
            public readonly int dy;
            public readonly uint mouseData;
            public readonly uint dwFlags;
            public readonly uint time;
            public readonly IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyboardInput
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public readonly uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public readonly uint uMsg;
            public readonly ushort wParamL;
            public readonly ushort wParamH;
        }
    }
}
