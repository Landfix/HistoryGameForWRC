using UnityEngine;

namespace Pool_container
{
    public interface IPool<T> where T : MonoBehaviour
    {
        T GetElement(Vector2 spawnPos);
        void ReturnToPull(T element);
    }
}