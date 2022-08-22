using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIOverObject : MonoBehaviour
{
    public UnityEvent OnEnterOver;
    public UnityEvent OnStayOver;
    public UnityEvent OnExitOver;

    private bool _isOver;
    private PointerEventData _pointerEventData;
    
    private void Start()
    {
        _pointerEventData = new PointerEventData(EventSystem.current);
    }

    private void Update()
    {
        if (IsMouseOverThis())
        {
            if (!_isOver)
            {
                OnEnterOver?.Invoke();
                _isOver = true;
            }
            OnStayOver?.Invoke();
        }
        else
        {
            if (_isOver)
            {
                OnExitOver?.Invoke();
                _isOver = false;
            }
        }
    }

    private bool IsMouseOverThis()
    {
        _pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultsList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_pointerEventData, raycastResultsList);

        bool value = raycastResultsList.Exists(r => r.gameObject.Equals(this.gameObject));

        return value;
    }
}
