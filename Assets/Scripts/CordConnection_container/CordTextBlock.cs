using TMPro;
using UnityEngine;

namespace CordConnection_container
{
    public class CordTextBlock : MonoBehaviour
    {
        [SerializeField,TextArea] private string _str; // multiline
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Collider2D _collider;
        
        public void Initialize()
        {
            _text.text = _str;
        }

        public bool CheckConnect(Vector2 pos)
        {
            return _collider.OverlapPoint(pos);
        }
    }
}