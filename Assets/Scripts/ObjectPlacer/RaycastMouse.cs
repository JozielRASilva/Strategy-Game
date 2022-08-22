using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZombieDiorama.ObjectPlacer
{
    public class RaycastMouse : MonoBehaviour
    {
        private Camera _mainCamera;

        private RaycastHit _hit;
        private Ray _ray;

        private int _uiLayer;
        private List<RaycastResult> _raycastResults = new List<RaycastResult>();
        private PointerEventData _eventData;

        private void Start()
        {
            if (!_mainCamera)
                _mainCamera = Camera.main;

            _uiLayer = LayerMask.NameToLayer("UI");
        }

        public Vector3 GetPosition(LayerMask mask)
        {
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, mask))
            {
                Vector3 objectHit = _hit.point;
                return objectHit;
            }
            return Vector2.zero;
        }

        public bool ValidPosition(LayerMask mask)
        {
            if (IsPointerOverUIElement())
                return false;

            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, mask))
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
                if (currentRaycastResult.gameObject.layer == _uiLayer)
                    return true;
            }
            return false;
        }

        private List<RaycastResult> GetEventSystemRaycastResults()
        {
            _eventData = new PointerEventData(EventSystem.current);
            _eventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(_eventData, _raycastResults);
            return _raycastResults;
        }

    }
}