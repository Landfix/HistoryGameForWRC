using System.Collections.Generic;
using UnityEngine;

namespace LineSequence_container.Configs
{
    [CreateAssetMenu(fileName = "CableConfigs",menuName = "Configs/CableConfigs")]
    public class CableConfigs : ScriptableObject
    {
        [SerializeField] private List<CableConfig> _cables;

        public List<CableConfig> Cables => _cables;
    }
}