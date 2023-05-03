using System;
using TMPro;
using UnityEngine;

namespace LineSequence_container
{
    public class CableBlock : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _numberText;

        public void Initialize(int number)
        {
            _numberText.text = number.ToString();
        }
    }
}