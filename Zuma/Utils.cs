namespace Zuma
{
    public class Utils
    {
        public static string GetDisplayName(string to)
        {
            return "John Doe";
        }

        public static string GetSub(string messageType)
        {
            switch (messageType)
            {
                case "welcome":
                    return "Welcome to Acme";
                case "forgotPassword":
                    return "Password Recovery";
                default:
                    return "Thanks for being a customer";
            }
        }

        public static string GetMsg(string messageType)
        {
            switch (messageType)
            {
                case "welcome":
                    return @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod
tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam,
quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo
consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse
cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non
proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
                case "forgotPassword":
                    return @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod
tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam,
quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo
consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse
cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non
proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
                default:
                    return @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod
tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam,
quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo
consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse
cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non
proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            }
        }

        public static string GetMsg2(string messageType)
        {
            switch (messageType)
            {
                case "welcome":
                    return @"Lorem ipsum dolor sit amet, consectetur adipisicing elit.";
                case "forgotPassword":
                    return @"Ut enim ad minim veniam, quis nostrud exercitation ullamco.";
                default:
                    return @"Duis aute irure dolor in reprehenderit in voluptate velit.";
            }
        }

        public static string GetEmailAddress(string to)
        {
            return "jdoe@geemail.com";
        }

        public static string GetNum(string user)
        {
            // lookup the users phone number in the database based on their username
            // and return it. Make sure the number is formatted +N(NNN)NNN-NNNN
            return "+14253123859";
        }
    }
}