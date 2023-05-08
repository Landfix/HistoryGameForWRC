using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CableSequence_container.Configs;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace CableSequence_container
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

        [Header("Components")] [SerializeField]
        private List<CableBlock> _cableBlocks;

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
                Cable cable = Instantiate(_cablePrefab, _cableContainer);
                _cableBlocks[i].Initialize(i + 1);
                cable.Initialize(_camera, _cableBlocks[i], cableConfig,i);
                cable.Up += Up;
                _cables.Add(cable);
            }
        }

        private void Up(Cable currentCable, Vector2 pos)
        {
            Cable cable = _cables.FirstOrDefault(x => x.Collider2D.OverlapPoint(pos) && x != currentCable);

            if (!cable)
            {
                currentCable.Move(currentCable.StartPos);
                return;
            }
            
            SwapCables(currentCable, cable);

            if (_cables.All(x => x.IsCorrectIndex)) 
                StartCoroutine(AllConnectedCoroutine());
        }

        private void SwapCables(Cable currentCable, Cable cable)
        {
            int fromIndex = _cables.IndexOf(currentCable);
            int toIndex = _cables.IndexOf(cable);

            _cables[fromIndex] = cable;
            _cables[toIndex] = currentCable;

            CableBlock savedCableBlock = cable.GetCableBlock();
            cable.SetNewBlock(currentCable.GetCableBlock(),fromIndex);
            currentCable.SetNewBlock(savedCableBlock,toIndex);


            currentCable.Move(cable.StartPos);
            cable.Move(currentCable.StartPos);
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
        
        private IEnumerator AllConnectedCoroutine()
        {
            // todo recast
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < _cables.Count; i++) 
                _cables[i].Up -= Up;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}