using System;
using UI;
using UnityEngine;

namespace Similarity_container
{
    public class BootstrapperOfSimilarity : BaseBootstrapper
    {
        [SerializeField] private GameUi _gameUi;
        [SerializeField] private Similarity _similarity;
        
        public override event Action Won;
        public override event Action Incorrected;

        private void Start()
        {
            _gameUi.Initialize(this);
            _similarity.Initialize();
            _similarity.Inconnected += Incorrected;
            _similarity.AllConnected += Won;
        }

        public override void ShowHint()
        {
        }
    }
}