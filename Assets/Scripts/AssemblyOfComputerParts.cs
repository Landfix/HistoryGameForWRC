using UnityEngine;

public class AssemblyOfComputerParts : MonoBehaviour
{
    [SerializeField] private ComputerPart[] _computerParts;
    [SerializeField] private ComputerShadowPart[] _computerShadowParts;

    private Camera _camera;
    public void Initialize()
    {
        _camera = Camera.main;

        for (int i = 0; i < _computerParts.Length; i++)
        {
            _computerParts[i].Initialize(_camera,_computerShadowParts[i]);
        }
    }
}