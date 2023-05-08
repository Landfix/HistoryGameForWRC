using UI;
using UnityEngine;

namespace AssemblyOfComputer_container
{
    public class BootstrapperOfAssembly : MonoBehaviour
    {
        [SerializeField] private GameUi _gameUi;
        [SerializeField] AssemblyOfComputer _assemblyOfComputer;
    
        private void Start()
        {
            _gameUi.Initialize();
            _assemblyOfComputer.Initialize();
        }
    }
}