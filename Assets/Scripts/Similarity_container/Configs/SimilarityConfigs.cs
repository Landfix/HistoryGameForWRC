using UnityEngine;

namespace Similarity_container.Configs
{
    [CreateAssetMenu(fileName = "SimilarityConfigs",menuName = "Configs/Similarity/SimilarityConfigs")]
    public class SimilarityConfigs : ScriptableObject
    {
        [SerializeField] private SimilarityElementConfig[] _similarityElementsFromConfig;

        public SimilarityElementConfig[] SimilarityElementsFromConfig => _similarityElementsFromConfig;
    }
}