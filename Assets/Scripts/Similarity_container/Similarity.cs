using System.Collections.Generic;
using System.Linq;
using Similarity_container.Configs;
using UnityEngine;

namespace Similarity_container
{
    public class Similarity : MonoBehaviour
    {
        [SerializeField] private SimilarityConfigs _similarityConfigs;
        [SerializeField] private SimilarityElement[] _leftElements;
        [SerializeField] private SimilarityElement[] _rightElements;

        private List<SimilarityElementConfig> _leftElementsByConfig;
        private List<SimilarityElementConfig> _rightElementsByConfig;

        private SimilarityElementConfig[] _similarityElementsFromConfig;
        private Camera _camera;
        
        public void Initialize()
        {
            DistributionByConfigs();
            InitializeLeftElements();
            InitializeRightElements();
        }

        private void InitializeRightElements()
        {
            for (int i = 0; i < _rightElements.Length; i++)
            {
                _rightElements[i].Initialize(_camera, _rightElementsByConfig[i]);
                _rightElements[i].Up += Up;
            }
        }

        private void InitializeLeftElements()
        {
            for (int i = 0; i < _leftElements.Length; i++)
            {
                _leftElements[i].Initialize(_camera, _leftElementsByConfig[i]);
                _leftElements[i].Up += Up;
            }
        }

        private void DistributionByConfigs()
        {
            _camera = Camera.main;
            _similarityElementsFromConfig = _similarityConfigs.SimilarityElementsFromConfig;
            _leftElementsByConfig = new List<SimilarityElementConfig>
                (_similarityElementsFromConfig.Where(x => x.SideType == SideType.Left));
            _rightElementsByConfig = new List<SimilarityElementConfig>
                (_similarityElementsFromConfig.Where(x => x.SideType == SideType.Right));
        }

        private void Up(SimilarityElement similarityElement, Vector2 pos)
        {
            SimilarityElement selectedSimilarityElement = GetSimilarityElement(similarityElement, pos);

            if (selectedSimilarityElement == null)
            {
                similarityElement.ResetPosition();
                return;
            }

            similarityElement.SelectCorrectArea();
            selectedSimilarityElement.SelectCorrectArea();
            similarityElement.Up -= Up;
            selectedSimilarityElement.Up -= Up;
        }

        private SimilarityElement GetSimilarityElement(SimilarityElement similarityElement, Vector2 pos)
        {
            SimilarityElement selectedSimilarityElement = similarityElement.SideType == SideType.Left
                ? _rightElements
                    .FirstOrDefault(x => x.Collider2D.OverlapPoint(pos) 
                                         && x != similarityElement 
                                         && x.SimilarityType == similarityElement.SimilarityType)
                : _leftElements
                    .FirstOrDefault(x => x.Collider2D.OverlapPoint(pos) 
                                         && x != similarityElement
                                         && x.SimilarityType == similarityElement.SimilarityType);
            
            return selectedSimilarityElement;
        }
    }
}