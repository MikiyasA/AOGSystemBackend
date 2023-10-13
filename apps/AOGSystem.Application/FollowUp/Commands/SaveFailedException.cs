using System.Runtime.Serialization;

namespace AOGSystem.Application.FollowUp.Commands
{
    [Serializable]
    internal class SaveFailedException : Exception
    {
        public SaveFailedException()
        {
        }

        public SaveFailedException(string? message) : base(message)
        {
        }

        public SaveFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SaveFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}