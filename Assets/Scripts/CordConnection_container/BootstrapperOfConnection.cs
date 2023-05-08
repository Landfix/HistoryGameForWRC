using UI;
using UnityEngine;

namespace CordConnection_container
{
    public class BootstrapperOfConnection : MonoBehaviour
    {
        [SerializeField] private GameUi _gameUi;
        [SerializeField] private CordConnection _cordConnection;

        private void Start()
        {
            _gameUi.Initialize();
            _cordConnection.Initialize();
        }
    }
}