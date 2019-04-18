namespace TokenAuthentication.Example.Events
{
    public class UserSessionEndedMessage
    {
        public enum SessionEndingType
        {
            Logout,
            SwitchUser
        }

        public UserSessionEndedMessage(SessionEndingType endingType)
        {
            EndingType = endingType;
        }

        public SessionEndingType EndingType { get; }
    }
}
