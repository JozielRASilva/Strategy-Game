using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZombieDiorama.Utilities.Patterns
{
    public static class Observer
    {
        private static Action<EventType, object> subscriptions;

        public static void Subscribe(Action<EventType, object> method)
        {
            subscriptions += method;
        }

        public static void UnSubscribe(Action<EventType, object> method)
        {
            subscriptions -= method;
        }

        public static void Notify(EventType tag, object value = null)
        {
            subscriptions.Invoke(tag, value);
        }
    }
}