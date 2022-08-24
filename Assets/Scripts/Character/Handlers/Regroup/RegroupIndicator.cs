using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using ZombieDiorama.ObjectPlacer;
using ZombieDiorama.Utilities.Patterns;
using System.Collections.Generic;

namespace ZombieDiorama.Character.Handler.Regroup
{
    public class RegroupIndicator : MonoBehaviour
    {
        [Title("Set Conditions")]
        public LayerMask WhereCanSet;

        [Title("Enable")]
        public bool startActivated = true;
        public bool AlwaysActivated = false;

        [Title("Observer Events")]
        public List<ObserverEvent> EventsToLock = new List<ObserverEvent>();
        public List<ObserverEvent> EventsToUnLock = new List<ObserverEvent>();

        private bool activated = true;
        private bool locked = false;

        private RaycastMouse raycastMouse;

        [Title("UI Feedback")]
        public bool hideWithInvalid;

        public Transform rectTransform;
        public Vector3 rotation = new Vector3(0, 0, 0);
        public Vector3 positionOffset = new Vector3(0, 0.2f, 0);


        [Title("Events")]
        public UnityEvent OnSet;
        public UnityEvent OnCanNotSet;
        public UnityEvent OnStopSet;

        private Vector3 lastPoint;

        private void Awake()
        {
            raycastMouse = GetComponent<RaycastMouse>();

            if (startActivated)
                Indicate();
            else
                StopIndicate();
        }

        private void Update()
        {
            if (locked || (!activated && !AlwaysActivated) || !raycastMouse)
                return;

            Vector3 point;
            if (raycastMouse.ValidPosition(WhereCanSet))
                point = raycastMouse.GetPosition(WhereCanSet);
            else point = lastPoint;

            ShowCanvasFeedback(point);

            if (hideWithInvalid && !raycastMouse.ValidPosition(WhereCanSet))
            {
                HideCanvasFeedback();
                return;
            }

            lastPoint = point;

            IndicatorReadInput();
        }

        private void IndicatorReadInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RegroupHandler.Instance.SetPoint(lastPoint);

                OnSet?.Invoke();

                if (!AlwaysActivated)
                    StopIndicate();

                return;
            }

            if (AlwaysActivated)
                return;

            if (Input.GetMouseButtonDown(1))
            {
                OnStopSet?.Invoke();
                StopIndicate();
            }
        }


        private void ShowCanvasFeedback(Vector3 point)
        {
            rectTransform.gameObject.SetActive(true);

            rectTransform.transform.position = point + positionOffset;
            rectTransform.LookAt(Camera.main.transform);
            rectTransform.eulerAngles = new Vector3(90 + rotation.x, rectTransform.eulerAngles.y + rotation.y, rectTransform.eulerAngles.z + rotation.z);
        }


        private void HideCanvasFeedback()
        {
            rectTransform.gameObject.SetActive(false);
        }

        public void Indicate()
        {
            activated = true;
        }

        public void StopIndicate()
        {
            activated = false;
            HideCanvasFeedback();
        }

        private void OnEnable()
        {
            Observer.Subscribe(ReadEvents);
        }

        private void OnDisable()
        {
            Observer.UnSubscribe(ReadEvents);
        }

        private void ReadEvents(ObserverEvent eventType, object o = null)
        {
            if (EventsToLock.Contains(eventType))
            {
                LockIndicator();
            }
            else if (EventsToUnLock.Contains(eventType))
            {
                UnlockIndicator();
            }
        }

        private void UnlockIndicator()
        {
            locked = false;
            Indicate();
        }

        private void LockIndicator()
        {
            locked = true;
            StopIndicate();
        }
    }
}
