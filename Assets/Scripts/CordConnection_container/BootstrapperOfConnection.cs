﻿using System;
using UI;
using UnityEngine;

namespace CordConnection_container
{
    public class BootstrapperOfConnection : BaseBootstrapper
    {
        [SerializeField] private GameUi _gameUi;
        [SerializeField] private CordConnection _cordConnection;

        public override event Action Won;
        public override event Action Incorrected;

        private void Start()
        {
            _gameUi.Initialize(this);
            _cordConnection.Initialize();
            _cordConnection.AllConnected += Won;
        }

        public override void ShowHint()
        {
            
        }
    }
}