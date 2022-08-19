using System.Collections;
using System.Collections.Generic;
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
        public Vector3 positionOffset = new Vector3(0, 0.2f, 0);
        public SettableObjectInfo settable;


        [Title("Rotation Feedback")]
        public RectTransform rectTransform;
        public Vector3 rotation = new Vector3(0, 0, 0);
        public Vector3 rotateValue;


        [Title("Enable")]
        public bool startActivated = true;

        private bool activated = true;

        private RaycastMouse raycastMouse;
        private SettableObjectPreview current;


        [Title("Events")]
        public UnityEvent OnSet;
        public UnityEvent OnCanNotSet;
        public UnityEvent OnStopSet;

        private void Awake()
        {
            raycastMouse = GetComponent<RaycastMouse>();

            if (startActivated)
                EnableSetter();
            else
                DisableSetter();
        }

        private void Update()
        {
            if (!activated) return;

            if (!settable || !raycastMouse) return;

            Vector3 point = raycastMouse.GetPosition(WhereCanSet);

            PreviewObjectPosition(point);

            SetterActions();

            if (raycastMouse.ValidPosition(WhereCanSet))
                ShowCanvasFeedback(point);
            else
                HideCanvasFeedback();

            SetObject();

        }


        public void SetObject()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (current.CanSet() && raycastMouse.ValidPosition(WhereCanSet))
                {
                    ObjectSetterManager.Instance.AddObjectToSet(current.transform, settable);

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
            current?.Rotate(rotateValue * horizontal);
        }

        private void ShowCanvasFeedback(Vector3 point)
        {
            if (!current)
            {
                HideCanvasFeedback();
                return;
            }
            else
            {
                if (!current.canRotate)
                {
                    HideCanvasFeedback();
                    return;
                }
            }

            rectTransform.gameObject.SetActive(true);

            rectTransform.transform.position = point + positionOffset;
            rectTransform.LookAt(Camera.main.transform);
            rectTransform.eulerAngles = new Vector3(90 + rotation.x, rectTransform.eulerAngles.y + rotation.y, rectTransform.eulerAngles.z + rotation.z);
        }

        private void HideCanvasFeedback()
        {
            rectTransform.gameObject.SetActive(false);
        }

        private void PreviewObjectPosition(Vector3 point)
        {

            if (!ObjectSetterManager.Instance) return;

            if (!current) current = ObjectSetterManager.Instance?.GetPreviewObject(settable);

            if (!current) return;

            if (!raycastMouse)
            {

                current?.gameObject?.SetActive(false);

                return;
            }
            else if (!raycastMouse.ValidPosition(WhereCanSet))
            {
                current?.gameObject?.SetActive(false);

                return;
            }


            current?.gameObject?.SetActive(true);
            current?.ShowPreview(point + positionOffset);

        }

        public void IndicateObjectToSet(SettableObjectInfo info)
        {
            settable = info;

            EnableSetter();
        }

        public void StopIndicateObjectToSet()
        {
            DisableSetter();
        }


        private void EnableSetter()
        {
            activated = true;

            current?.EnableObject();
        }

        private void DisableSetter()
        {
            activated = false;

            current?.DisableObject();

            current = null;

            HideCanvasFeedback();
        }
    }
}