using UnityEngine;

public class ComputerPart : MonoBehaviour
{
    [SerializeField] private Collider2D _collider2D;
    
    private Camera _camera;
    private ComputerShadowPart _computerShadowPart;
    private Vector2 _dragOffset;
    
    public void Initialize(Camera camera, ComputerShadowPart computerShadowPart)
    {
        _camera = camera;
        _computerShadowPart = computerShadowPart;
    }

    private void OnMouseDown()
    {
        _dragOffset = (Vector2)transform.position - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePosition() + _dragOffset;
    }

    private void OnMouseUp()
    {
        if (_computerShadowPart.Collider2D.OverlapPoint(GetMousePosition()))
        {
            transform.position = _computerShadowPart.transform.position;
        }
    }

    private Vector2 GetMousePosition()
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }
}