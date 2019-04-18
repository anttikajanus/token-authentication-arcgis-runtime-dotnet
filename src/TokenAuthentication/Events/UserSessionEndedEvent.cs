using Prism.Events;

namespace TokenAuthentication.Example.Events
{
    public class UserSessionEndedEvent : PubSubEvent<UserSessionEndedMessage> {}
}
