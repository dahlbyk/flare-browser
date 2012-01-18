using System;

namespace Flare
{
    public class Message
    {
        public Message(string name, string message, string elementId)
        {
            Name = name;
            TextMessage = message;
            ElementId = elementId;
        }

        public String Name { get; set; }
        public String TextMessage { get; set; }
        public String ElementId { get; set; }
    }
}