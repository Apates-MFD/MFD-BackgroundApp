
namespace EDLibrary.PipeSystem
{
    /// <summary>
    /// Abstract PipeWrite Class
    /// </summary>
    abstract class PipeWrite : Pipe
    {
        public abstract void Write(object data);
    }
}
