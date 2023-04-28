using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class NextSceneBtn : MonoBehaviour
    {
        [SerializeField] private Button _btn;
        private void Start()
        {
            _btn.onClick.AddListener(OnClickNextScene);
        }

        private void OnClickNextScene()
        {
            _btn.onClick.RemoveListener(OnClickNextScene);
            if (SceneManager.sceneCount - 1 > SceneManager.GetActiveScene().buildIndex)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(0);
        }
    }
}