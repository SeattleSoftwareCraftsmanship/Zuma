using System;

namespace Zuma
{
    public class Logger
    {
        public void CreateNewOrOpenExistingLogAndWriteToLog(Type type, string message)
        {
            // Check if a logger exists for the type of object we want to log
            // cast the message to the right type for the logger
            // if it exists, open it and write the message to it
            // if it doesn't create a new one and write the message to it
        }

        public void Close()
        {
            // Close Logger
        }
    }

    public class MessageLog
    {
        
    }
}