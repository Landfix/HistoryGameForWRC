using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AssemblyOfComputer_container
{
    public class AssemblyOfComputer : MonoBehaviour
    {
        [SerializeField] private ComputerPart[] _computerParts;
        [SerializeField] private ComputerShadowPart[] _computerShadowParts;

        private Camera _camera;

        public event Action AllConnected;
        public event Action Inconnected;
        
        public void Initialize()
        {
            _camera = Camera.main;

            for (int i = 0; i < _computerParts.Length; i++)
            {
                _computerParts[i].Initialize(_camera,_computerShadowParts[i]);
                _computerParts[i].Connected += CheckAllConnects;
                _computerParts[i].Inconnected += Inconnected;
            }
        }

        private void CheckAllConnects()
        {
            if (_computerParts.All(x => x.IsConnect))
            {
                StartCoroutine(ActivateNewLevelCoroutine());
                AllConnected?.Invoke();
            }
        }

        private IEnumerator ActivateNewLevelCoroutine()
        {
            yield return new WaitForSeconds(0.7f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}