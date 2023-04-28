using System;
using System.Collections.Generic;
using System.Linq;
using LineSequence_container.Configs;
using UnityEngine;
using Random = System.Random;

namespace LineSequence_container
{
    public enum CableType
    {
        WhiteOrange = 0,
        Orange,
        WhiteGreen,
        Blue,
        WhiteBlue,
        Green,
        WhiteBrown,
        Brown
    }

    public class CableSequence : MonoBehaviour
    {
        [SerializeField] private CableConfigs _cableConfigs;
        [Header("Components")]
        [SerializeField] private List<CableBlock> _cableBlocks;
        [SerializeField] private Cable _cablePrefab;
        [SerializeField] private Transform _cableContainer;

        private List<CableType> _cableTypes;
        private List<Cable> _cables;
        private Camera _camera;

        public void Start()
        {
            _camera = Camera.main;
            _cables = new List<Cable>();
            CreateCableTypes();
            
            for (int i = 0; i < _cableTypes.Count; i++)
            {
                CableConfig cableConfig = _cableConfigs.Cables.FirstOrDefault(x => x.CableType == _cableTypes[i]);
                Cable cable = Instantiate(_cablePrefab,_cableContainer);
                cable.Initialize(_camera,_cableBlocks[i],cableConfig);
                _cables.Add(cable);
            }
        }

        private void CreateCableTypes()
        {
            _cableTypes = new List<CableType>();
            int numberOfCable = Enum.GetValues(typeof(CableType)).Length;

            for (int i = 0; i < numberOfCable; i++)
                _cableTypes.Add((CableType) i);

            var rnd = new Random();
            var shuffledCableTypes = _cableTypes.OrderBy(a => rnd.Next());
            _cableTypes = shuffledCableTypes.ToList();
        }
    }
}