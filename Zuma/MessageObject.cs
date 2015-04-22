namespace Zuma
{
    internal class MessageObject
    {
        public string Message { get; private set; }
        public string ShortMessage { get; private set; }
        public string Subject { get; private set; }

        public static MessageObject Create(string messageType)
        {
            return new MessageObject
            {
                Message = Utils.GetMsg(messageType),
                ShortMessage = Utils.GetMsg2(messageType),
                Subject = Utils.GetSub(messageType)
            };
        }
    }
}