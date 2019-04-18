using Prism.Events;

namespace TokenAuthentication.Example.Events
{
    public class UserSessionCreatedEvent : PubSubEvent<UserSessionCreatedMessage> {}
}
