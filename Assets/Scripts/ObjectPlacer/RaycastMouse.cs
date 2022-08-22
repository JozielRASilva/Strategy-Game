using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZombieDiorama.ObjectPlacer
{
    public class RaycastMouse : MonoBehaviour
    {
        private Camera mainCamera;
        private Vector3 currentPoint;

        private RaycastHit hit;
        private Ray ray;

        private int uiLayer;
        private List<RaycastResult> raycastResults = new List<RaycastResult>();
        private PointerEventData eventData;

        private void Start()
        {
            if (!mainCamera)
                mainCamera = Camera.main;

            uiLayer = LayerMask.NameToLayer("UI");
        }

        public Vector3 GetPosition(LayerMask mask)
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                Vector3 objectHit = hit.point;
                return objectHit;
            }
            return Vector2.zero;
        }

        public bool ValidPosition(LayerMask mask)
        {
            if (IsPointerOverUIElement())
                return false;

            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                return true;
            }
            return false;
        }

        public bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }

        private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaycastResults)
        {
            for (int index = 0; index < eventSystemRaycastResults.Count; index++)
            {
                RaycastResult currentRaycastResult = eventSystemRaycastResults[index];
                Debug.Log($"{currentRaycastResult.gameObject.name}");
                if (currentRaycastResult.gameObject.layer == uiLayer)
                    return true;
            }
            return false;
        }

        private List<RaycastResult> GetEventSystemRaycastResults()
        {
            eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(eventData, raycastResults);
            return raycastResults;
        }

    }
}