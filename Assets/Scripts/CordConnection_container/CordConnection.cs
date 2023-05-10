using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CordConnection_container
{
    public class CordConnection : MonoBehaviour
    {
        [SerializeField] private Cord[] _cords;
        [SerializeField] private CordTextBlock[] _cordTextBlocks;

        private Camera _camera;

        public event Action AllConnected;

        public void Initialize()
        {
            _camera = Camera.main;

            for (int i = 0; i < _cords.Length; i++)
            {
                _cordTextBlocks[i].Initialize();
                _cords[i].Initialize(_cordTextBlocks[i],_camera);
                _cords[i].Connected += Connected;
            }
        }

        private void Connected()
        {
            if (_cords.All(x => x.IsConnect))
            {
                AllConnected?.Invoke();
                StartCoroutine(AllConnectedCoroutine());
            }
        }

        private IEnumerator AllConnectedCoroutine()
        {
            // todo recast
            yield return new WaitForSeconds(0.7f);
            for (int i = 0; i < _cords.Length; i++) 
                _cords[i].Connected -= Connected;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}