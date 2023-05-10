using UnityEngine;

namespace UI
{
    public class GameUi : MonoBehaviour
    {
        [SerializeField] private GameContainer _gameContainer;
        [SerializeField] private WinCurtain _winCurtain;
        [SerializeField] private IncorrectCurtain _incorrectCurtain;
        
        public void Initialize(BaseBootstrapper baseBootstrapper)
        {
            _gameContainer.Initialize(baseBootstrapper);
            _winCurtain.Initialize(baseBootstrapper);
            _incorrectCurtain.Initialize(baseBootstrapper);
        }
    }
}