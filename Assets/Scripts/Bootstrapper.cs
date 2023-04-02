using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameUi _gameUi;
    [SerializeField] AssemblyOfComputerParts assemblyOfComputerParts;
    
    private void Start()
    {
        _gameUi.Initialize();
        assemblyOfComputerParts.Initialize();
    }
}