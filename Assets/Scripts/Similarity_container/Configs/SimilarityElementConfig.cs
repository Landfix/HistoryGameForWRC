using UnityEngine;

namespace Similarity_container.Configs
{
    public enum SimilarityType
    {
        A,
        B,
        C,
        D
    }

    public enum SideType
    {
        Left,
        Right
    }

    [CreateAssetMenu(fileName = "SimilarityConfig",menuName = "Configs/Similarity/SimilarityConfig")]
    public class SimilarityElementConfig : ScriptableObject
    {
        [SerializeField] private SimilarityType _similarityType;
        [SerializeField] private SideType _sideType;
        [SerializeField] private Sprite _sprite;

        public SimilarityType SimilarityType => _similarityType;
        public Sprite Sprite => _sprite;
        public SideType SideType => _sideType;
    }
}