using UnityEngine;

public class ComputerShadowPart : MonoBehaviour
{
    [SerializeField] private Collider2D _collider2D;

    public Collider2D Collider2D => _collider2D;
}