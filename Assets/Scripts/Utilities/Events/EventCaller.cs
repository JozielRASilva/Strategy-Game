using UnityEngine;
using UnityEngine.Events;

namespace ZombieDiorama.Utilities.Events
{
    public class EventCaller : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _firstCaller;

        [SerializeField]
        private UnityEvent _secondCaller;

        public void FirstCall()
        {
            _firstCaller?.Invoke();
        }

        public void SecondCall()
        {
            _secondCaller?.Invoke();
        }
    }
}