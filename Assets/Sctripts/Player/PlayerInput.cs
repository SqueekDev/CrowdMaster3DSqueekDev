using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private LayerMask _layerMask;

    private Vector3 _tapPosition;
    private Camera _camera;

    public event UnityAction<Vector2> DirectionChanged;
    public event UnityAction PointerUp;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
            {
                _tapPosition = raycastHit.point;
                Vector2 direction = new Vector2(_tapPosition.x - _playerTransform.position.x, _tapPosition.z - _playerTransform.position.z);                
                DirectionChanged?.Invoke(direction);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            PointerUp?.Invoke();
        }
    }
}
