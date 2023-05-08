using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Localization_container
{
    public class Localizable : MonoBehaviour {
        public string key;
        public object[] parameters = null;

        private TMP_Text _text;
        private Localization _localization => Localization.Instance;

        void OnEnable() {
            _text = GetComponent<TMP_Text>();
            UpdateText();
            _localization.OnLanguageChange += UpdateText;
        }
        void OnDisable() {
            if (_localization)
                _localization.OnLanguageChange -= UpdateText;
        }
        public void UpdateText() {
            var localizedText = _localization.Get(key, parameters);

            if (!_text) _text = GetComponent<TMP_Text>();
            _text.text = localizedText;

            var fitter = GetComponent<ContentSizeFitter>();
            if (fitter != null) fitter.SetLayoutHorizontal();
        }
        public void UpdateKey(string newKey) {
            key = newKey;
            UpdateText();
        }
        public void UpdateParams(params object[] parameters) {
            this.parameters = parameters;
            UpdateText();
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/UI/Localizable")]
        static void CreateLocalizable() {
            var go = new GameObject();
            go.AddComponent<CanvasRenderer>();
            go.AddComponent<Localizable>();
            go.AddComponent<TextMeshProUGUI>();
            GameObjectUtility.SetParentAndAlign(go, Selection.activeGameObject);
            Selection.activeGameObject = go;
        }
#endif
    }
}