using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ZombieDiorama.UI
{
    public class UIOverObject : MonoBehaviour
    {
        public UnityEvent OnEnterOver;
        public UnityEvent OnStayOver;
        public UnityEvent OnExitOver;

        private bool isOver;

        PointerEventData pointerEventData;

        private void Start()
        {
            pointerEventData = new PointerEventData(EventSystem.current);
        }

        private void Update()
        {
            if (IsMouseOverThis())
            {
                if (!isOver)
                {
                    OnEnterOver?.Invoke();
                    isOver = true;
                }
                OnStayOver?.Invoke();
            }
            else
            {
                if (isOver)
                {
                    OnExitOver?.Invoke();
                    isOver = false;
                }
            }
        }

        private bool IsMouseOverThis()
        {
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> raycastResultsList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);

            bool value = raycastResultsList.Exists(r => r.gameObject.Equals(this.gameObject));

            return value;
        }
    }
}