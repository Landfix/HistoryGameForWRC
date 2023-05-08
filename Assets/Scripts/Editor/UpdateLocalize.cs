using Localization_container;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Localization))]
    public class UpdateLocalize : UnityEditor.Editor
    {
        private Localization _localization;
            
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
                
            GUILayout.Space(15);
            _localization = (Localization) target;
            GUI.backgroundColor = Color.cyan;

            if (GUILayout.Button("Update Localization",GUILayout.Width(400),GUILayout.Height(50))) 
                _localization.UpdateLocalization();
        }
    }
}