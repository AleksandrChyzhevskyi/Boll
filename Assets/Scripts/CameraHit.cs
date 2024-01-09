using UnityEngine;

public class CameraHit : MonoBehaviour
{
    private const int MaxDistance = 100;

    [SerializeField] private LayerMask _layerMask;

    private Camera _camera;

    private void Awake() =>
        _camera = Camera.main;

    public Circle GetCircle()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, MaxDistance, _layerMask) == false)
            return null;

        if (hit.transform.gameObject.TryGetComponent(out Circle circle) == false)
            return null;

        return circle;
    }
}