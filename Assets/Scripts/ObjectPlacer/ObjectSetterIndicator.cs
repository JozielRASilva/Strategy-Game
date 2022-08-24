using ZombieDiorama.Utilities.Patterns;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ZombieDiorama.ObjectPlacer
{
    [RequireComponent(typeof(RaycastMouse))]
    public class ObjectSetterIndicator : Singleton<ObjectSetterIndicator>
    {

        [Title("Set Conditions")]
        public LayerMask WhereCanSet;

        [Title("Object Info")]
        [FormerlySerializedAs("positionOffset")] public Vector3 PositionOffset = new Vector3(0, 0.2f, 0);
        [FormerlySerializedAs("settable")] public SettableObjectInfo Settable;


        [Title("Rotation Feedback")]
        [FormerlySerializedAs("rectTransform")] public RectTransform RotationRectTransform;
        [FormerlySerializedAs("rotation")] public Vector3 Rotation = new Vector3(0, 0, 0);
        [FormerlySerializedAs("rotateValue")] public Vector3 RotateValue;


        [Title("Enable")]
        [FormerlySerializedAs("startActivated")] public bool StartActivated = true;

        [FormerlySerializedAs("activated")] private bool Activated = true;

        [FormerlySerializedAs("raycastMouse")] private RaycastMouse RaycastMouse;
        [FormerlySerializedAs("current")] private SettableObjectPreview Current;

        [Title("Observer events")]
        public ObserverEvent SettingEvent;
        public ObserverEvent StopSettingEvent;


        [Title("Events")]
        public UnityEvent OnSet;
        public UnityEvent OnCanNotSet;
        public UnityEvent OnStopSet;

        protected override void Awake()
        {
            base.Awake();
            RaycastMouse = GetComponent<RaycastMouse>();

            if (StartActivated)
                EnableSetter();
            else
                DisableSetter();
        }

        private void Update()
        {
            if (!Activated) return;

            if (!Settable || !RaycastMouse) return;

            Vector3 point = RaycastMouse.GetPosition(WhereCanSet);

            PreviewObjectPosition(point);

            SetterActions();

            if (RaycastMouse.ValidPosition(WhereCanSet))
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
                if (Current.CanSet() && RaycastMouse.ValidPosition(WhereCanSet))
                {
                    ObjectSetterManager.Instance.AddObjectToSet(Current.transform, Settable);

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
            Current?.Rotate(RotateValue * horizontal);
        }

        private void ShowCanvasFeedback(Vector3 point)
        {
            if (!Current)
            {
                HideCanvasFeedback();
                return;
            }
            else
            {
                if (!Current.canRotate)
                {
                    HideCanvasFeedback();
                    return;
                }
            }

            RotationRectTransform.gameObject.SetActive(true);

            RotationRectTransform.transform.position = point + PositionOffset;
            RotationRectTransform.LookAt(Camera.main.transform);
            RotationRectTransform.eulerAngles = new Vector3(90 + Rotation.x, RotationRectTransform.eulerAngles.y + Rotation.y, RotationRectTransform.eulerAngles.z + Rotation.z);
        }

        private void HideCanvasFeedback()
        {
            RotationRectTransform.gameObject.SetActive(false);
        }

        private void PreviewObjectPosition(Vector3 point)
        {

            if (!ObjectSetterManager.Instance) return;

            if (!Current) Current = ObjectSetterManager.Instance?.GetPreviewObject(Settable);

            if (!Current) return;

            if (!RaycastMouse)
            {

                Current?.gameObject?.SetActive(false);

                return;
            }
            else if (!RaycastMouse.ValidPosition(WhereCanSet))
            {
                Current?.gameObject?.SetActive(false);

                return;
            }


            Current?.gameObject?.SetActive(true);
            Current?.ShowPreview(point + PositionOffset);

        }

        public static void IndicateObject(SettableObjectInfo info)
        {
            Instance?.IndicateObjectToSet(info);
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
            Activated = true;

            Current?.EnableObject();
        }

        private void DisableSetter()
        {
            Activated = false;

            Current?.DisableObject();

            Current = null;

            HideCanvasFeedback();
        }
    }
}