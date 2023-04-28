using UnityEngine;

namespace LineSequence_container.Configs
{
    [CreateAssetMenu(fileName = "CableConfig",menuName = "Configs/CableConfig")]
    public class CableConfig : ScriptableObject
    {
        [SerializeField] private Sprite _model;
        [SerializeField] private CableType _cableType;
        
        public Sprite Model => _model;
        public CableType CableType => _cableType;
    }
}