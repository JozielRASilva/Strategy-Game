using System;

namespace ZombieDiorama.Utilities.Patterns
{
    public static class Observer
    {
        private static Action<ObserverEvent, object> _subscriptions;

        public static void Subscribe(Action<ObserverEvent, object> method)
        {
            _subscriptions += method;
        }

        public static void UnSubscribe(Action<ObserverEvent, object> method)
        {
            _subscriptions -= method;
        }

        public static void Notify(ObserverEvent tag, object value = null)
        {
            _subscriptions.Invoke(tag, value);
        }
    }
}