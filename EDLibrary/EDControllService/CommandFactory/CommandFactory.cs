using EDLibrary.EDControllService.Menu;
using System;
using System.Collections.Generic;

namespace EDLibrary.EDControllService.CommandFactory
{
    public class CommandFactory
    {
        private Dictionary<Type, commandTypes> commandTypeMap = new Dictionary<Type, commandTypes>();
        private enum commandTypes
        {
            IN_GAME,
            CHANGE_MENU
        }

        public Command getCommand(SerializableCommand serialized)
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

        #region Singelton
        private static readonly CommandFactory instance = new CommandFactory();

        private CommandFactory()
        {
            //Populate commandTypeMap
            commandTypeMap.Add(typeof(InGameCommand), commandTypes.IN_GAME);
            commandTypeMap.Add(typeof(ChangeMenuCommand), commandTypes.CHANGE_MENU);
        }
        public static CommandFactory Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion
    }
}
