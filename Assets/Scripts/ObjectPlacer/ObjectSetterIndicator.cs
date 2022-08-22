using ZombieDiorama.Utilities.Patterns;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace ZombieDiorama.ObjectPlacer
{
    [RequireComponent(typeof(RaycastMouse))]
    public class ObjectSetterIndicator : MonoBehaviour
    {

        [Title("Set Conditions")]
        public LayerMask WhereCanSet;

        [Title("Object Info")]
        public Vector3 PositionOffset = new Vector3(0, 0.2f, 0);
        public SettableObjectInfo Settable;

        [Title("Rotation Feedback")]
        public RectTransform RotationModel;
        public Vector3 Rotation = new Vector3(0, 0, 0);
        public Vector3 RotateValue;

        [Title("Enable")]
        public bool StartActivated = true;

        [Title("Observer events")]
        public ObserverEvent SettingEvent;
        public ObserverEvent StopSettingEvent;

        [Title("Events")]
        public UnityEvent OnSet;
        public UnityEvent OnCanNotSet;
        public UnityEvent OnStopSet;

        private bool _activated = true;
        private RaycastMouse _raycastMouse;
        private SettableObjectPreview _current;

        private void Awake()
        {
            _raycastMouse = GetComponent<RaycastMouse>();

            if (StartActivated)
                EnableSetter();
            else
                DisableSetter();
        }

        private void Update()
        {
            if (!_activated) return;

            if (!Settable || !_raycastMouse) return;

            Vector3 point = _raycastMouse.GetPosition(WhereCanSet);

            PreviewObjectPosition(point);

            SetterActions();

            if (_raycastMouse.ValidPosition(WhereCanSet))
            {
                ShowCanvasFeedback(point);
            }
            else
            {
                HideCanvasFeedback();
                return;
            }

            SetObject();
        }


        public void SetObject()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_current.CanSet() && _raycastMouse.ValidPosition(WhereCanSet))
                {
                    ObjectSetterManager.Instance.AddObjectToSet(_current.transform, Settable);

                    OnSet?.Invoke();

                    StopIndicateObjectToSet();
                }
                else
                {
                    OnCanNotSet?.Invoke();
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                OnStopSet?.Invoke();

                StopIndicateObjectToSet();
            }
        }

        private void SetterActions()
        {
            float horizontal = Input.GetAxis("Horizontal");
            _current?.Rotate(RotateValue * horizontal);
        }

        private void ShowCanvasFeedback(Vector3 point)
        {
            if (!_current)
            {
                HideCanvasFeedback();
                return;
            }
            else
            {
                if (!_current.CanRotate)
                {
                    HideCanvasFeedback();
                    return;
                }
            }

            RotationModel.gameObject.SetActive(true);

            RotationModel.transform.position = point + PositionOffset;
            RotationModel.LookAt(Camera.main.transform);
            RotationModel.eulerAngles = new Vector3(90 + Rotation.x, RotationModel.eulerAngles.y + Rotation.y, RotationModel.eulerAngles.z + Rotation.z);
        }

        private void HideCanvasFeedback()
        {
            RotationModel.gameObject.SetActive(false);
        }

        private void PreviewObjectPosition(Vector3 point)
        {

            if (!ObjectSetterManager.Instance) return;

            if (!_current) _current = ObjectSetterManager.Instance?.GetPreviewObject(Settable);

            if (!_current) return;

            if (!_raycastMouse)
            {

                _current?.gameObject?.SetActive(false);

                return;
            }
            else if (!_raycastMouse.ValidPosition(WhereCanSet))
            {
                _current?.gameObject?.SetActive(false);

                return;
            }


            _current?.gameObject?.SetActive(true);
            _current?.ShowPreview(point + PositionOffset);

        }

        public void IndicateObjectToSet(SettableObjectInfo info)
        {
            Settable = info;

            Observer.Notify(SettingEvent);

            EnableSetter();
        }

        public void StopIndicateObjectToSet()
        {
            Observer.Notify(StopSettingEvent);

            DisableSetter();
        }


        private void EnableSetter()
        {
            _activated = true;

            _current?.EnableObject();
        }

        private void DisableSetter()
        {
            _activated = false;

            _current?.DisableObject();

            _current = null;

            HideCanvasFeedback();
        }
    }
}