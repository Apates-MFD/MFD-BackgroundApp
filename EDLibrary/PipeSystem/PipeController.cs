using System;
using System.Collections.Generic;

namespace EDLibrary.PipeSystem
{
    /// <summary>
    /// Controller for handling pipes
    /// </summary>
    class PipeController
    {
        private Dictionary<string, Pipe> registry = new Dictionary<string, Pipe>();

        /// <summary>
        /// Registers a new pipe if name is free
        /// </summary>
        /// <param name="pipe">Pipe object</param>
        /// <param name="name">Name of the pipe for registry entry</param>
        /// <exception cref="ArgumentException">Thrown when a pipe is already 
        /// registered with given name</exception>
        public void register(Pipe pipe, string name)
        {
            if (registry.ContainsKey(name)) throw new ArgumentException("Name is already taken");
            registry.Add(name, pipe);
        }

        /// <summary>
        /// Unregisters a pipe
        /// </summary>
        /// <param name="name">Name of the pipe</param>
        /// <exception cref="ArgumentException">Throws when trying to unregistering a free name</exception>
        public void unregister(string name)
        {
            if (!registry.ContainsKey(name)) throw new ArgumentException("Name is not taken");
            registry.Remove(name);
        }

        /// <summary>
        /// Get a pipe
        /// </summary>
        /// <param name="name"></param>
        /// <returns> <see cref="Pipe"/> if name is registered; otherwise <see langword="null"/></returns>
        public Pipe GetPipe(string name)
        {
            Pipe pipe;
            registry.TryGetValue(name, out pipe);
            return pipe;
        }

        #region Singelton
        private static readonly PipeController instance = new PipeController();

        private PipeController()
        {

        }
        public static PipeController Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion
    }
}
