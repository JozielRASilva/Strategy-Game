using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using ZombieDiorama.ObjectPlacer;
using ZombieDiorama.Utilities.Patterns;
using System.Collections.Generic;

namespace ZombieDiorama.Character.Controllers.Regroup
{
    public class RegroupIndicator : MonoBehaviour
    {
        [Title("Set Conditions")]
        public LayerMask WhereCanSet;

        [Title("Enable")]
        public bool StartActivated = true;
        public bool AlwaysActivated = false;

        [Title("Observer Events")]
        public List<ObserverEvent> EventsToLock = new List<ObserverEvent>();
        public List<ObserverEvent> EventsToUnLock = new List<ObserverEvent>();

        [Title("UI Feedback")]
        public bool HideWithInvalid;

        public Transform RectTransform;
        public Vector3 Rotation = new Vector3(0, 0, 0);
        public Vector3 PositionOffset = new Vector3(0, 0.2f, 0);

        [Title("Events")]
        public UnityEvent OnSet;
        public UnityEvent OnCanNotSet;
        public UnityEvent OnStopSet;

        private Vector3 _lastPoint;
        private bool _activated = true;
        private bool _locked = false;
        private RaycastMouse _raycastMouse;

        private void Awake()
        {
            _raycastMouse = GetComponent<RaycastMouse>();

            if (StartActivated)
                Indicate();
            else
                StopIndicate();
        }

        private void Update()
        {
            if (_locked || (!_activated && !AlwaysActivated) || !_raycastMouse)
                return;

            Vector3 point;
            if (_raycastMouse.ValidPosition(WhereCanSet))
                point = _raycastMouse.GetPosition(WhereCanSet);
            else point = _lastPoint;

            ShowCanvasFeedback(point);

            if (HideWithInvalid && !_raycastMouse.ValidPosition(WhereCanSet))
            {
                HideCanvasFeedback();
                return;
            }

            _lastPoint = point;

            IndicatorReadInput();
        }

        private void IndicatorReadInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RegroupController.Instance.SetPoint(_lastPoint);

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
            RectTransform.gameObject.SetActive(true);

            RectTransform.transform.position = point + PositionOffset;
            RectTransform.LookAt(Camera.main.transform);
            RectTransform.eulerAngles = new Vector3(90 + Rotation.x, RectTransform.eulerAngles.y + Rotation.y, RectTransform.eulerAngles.z + Rotation.z);
        }


        private void HideCanvasFeedback()
        {
            RectTransform.gameObject.SetActive(false);
        }

        public void Indicate()
        {
            _activated = true;
        }

        public void StopIndicate()
        {
            _activated = false;
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
            _locked = false;
            Indicate();
        }

        private void LockIndicator()
        {
            _locked = true;
            StopIndicate();
        }
    }
}
