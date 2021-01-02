using EDLibrary.Menu;
using System;
using System.Collections.Generic;

namespace EDLibrary.CommandFactory
{
    /// <summary>
    /// Parses and executes a command
    /// </summary>
    public static class Factory
    {
        private enum commandTypes
        {
            IN_GAME,
            CHANGE_MENU
        }

        private static Dictionary<Type, commandTypes> commandTypeMap = new Dictionary<Type, commandTypes>(){
                { typeof(InGameCommand), commandTypes.IN_GAME },
                { typeof(ChangeMenuCommand), commandTypes.CHANGE_MENU}
        };
        
        /// <summary>
        /// parses a serialized command and executes it
        /// </summary>
        /// <param name="serialized"></param>
        public static Command GetCommand(SerializableCommand serialized)
        {
            if (serialized == null) return null;
            dynamic command = null;

            switch (commandTypeMap[Type.GetType(serialized.CommandType)])
            {
                case commandTypes.IN_GAME:
                    command = new InGameCommand()
                    {
                        Action = Enum.Parse<Actions>(serialized.ParameterValues[0])
                    };

                    break;

                case commandTypes.CHANGE_MENU:
                    command = new ChangeMenuCommand()
                    {
                        MenuName = serialized.ParameterValues[1],
                        Caller = serialized.ParameterValues[0],
                    };
                    break;
            }
            return command;
        }
    }
}
