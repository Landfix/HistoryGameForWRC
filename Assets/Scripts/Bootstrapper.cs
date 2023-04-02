using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameUi _gameUi;
    [SerializeField] AssemblyOfComputer _assemblyOfComputer;
    
    private void Start()
    {
        _gameUi.Initialize();
        _assemblyOfComputer.Initialize();
    }
}