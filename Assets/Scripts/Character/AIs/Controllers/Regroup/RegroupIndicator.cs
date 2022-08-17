using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace ZombieDiorama.Character.AIs.Controllers.Regroup
{
    public class RegroupIndicator : MonoBehaviour
    {
        [Title("Set Conditions")]
        public LayerMask WhereCanSet;

        [Title("Enable")]
        public bool startActivated = true;

        private bool activated = true;

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
                Enable();
            else
                Disable();
        }

        private void Update()
        {
            if (!activated) return;

            if (!raycastMouse) return;


            Vector3 point;
            if (raycastMouse.ValidPosition(WhereCanSet))
                point = raycastMouse.GetPosition(WhereCanSet);
            else point = lastPoint;

            ShowCanvasFeedback(point);

            if (hideWithInvalid && !raycastMouse.ValidPosition(WhereCanSet))
                HideCanvasFeedback();


            lastPoint = point;

            SetObject();
        }

        private void SetObject()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RegroupController.Instance.SetPoint(lastPoint);

                OnSet?.Invoke();

                StopIndicate();

            }
            else if (Input.GetMouseButtonDown(1))
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
            Enable();
        }

        public void StopIndicate()
        {
            Disable();
        }


        private void Enable()
        {
            activated = true;

        }

        private void Disable()
        {
            activated = false;

            HideCanvasFeedback();
        }
    }
}
