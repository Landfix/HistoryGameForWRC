using System;
using UI;
using UnityEngine;

namespace CableSequence_container
{
    public class BootstrapperOfCableSequence : BaseBootstrapper
    {
        [SerializeField] private GameUi _gameUi;
        [SerializeField] private CableSequence _cableSequence;

        public override event Action Won;
        public override event Action Incorrected;
        
        private void Start()
        {
            _gameUi.Initialize(this);
            _cableSequence.Initialize();
        }

        public override void ShowHint()
        {
        }
    }
}