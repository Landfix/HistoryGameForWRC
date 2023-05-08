using TMPro;
using UnityEngine;

namespace CableSequence_container
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