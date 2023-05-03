using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pool_container
{
    public class Pool<T> : IPool<T> where T : MonoBehaviour
    {
        private Queue<PullElement> _pool;
        
        private readonly T _prefab;
        private readonly Transform _container;
        
        private readonly int _baseQuantity;
        private readonly int _additionQuantity;
        private readonly bool _isUpdateContainer;
        
        public Pool(T prefab, Transform container, int baseQuantity, int additionQuantity, bool isUpdateContainer = false)
        {
            _prefab = prefab;
            _container = container;
            _baseQuantity = baseQuantity;
            _additionQuantity = additionQuantity;
            _isUpdateContainer = isUpdateContainer;
            
            Initialize();
        }

        private void Initialize()
        {
            _pool = new Queue<PullElement>(_baseQuantity);
            
            SpawnElements(_baseQuantity);
        }

        public T GetElement(Vector2 newSpawn)
        {
            if (HasFreeElement(out T newElement, newSpawn))
                return newElement;

            SpawnElements(_additionQuantity);
            return GetElement(newSpawn);
        }

        public void ReturnToPull(T element)
        {
            var pullElement = _pool.FirstOrDefault(e => e.Element == element);
            if (pullElement != null)
            {
                pullElement.Used = false;
                element.gameObject.SetActive(false);
            }
        }

        private void SpawnElements(int quantity)
        {
            for (int i = 0; i < quantity; i++) 
                CreateElement();
        }

        private T CreateElement(bool isActiveByDefault = false)
        {
            T newElement = Object.Instantiate(_prefab, _container.position,Quaternion.identity, _container);
            
            newElement.gameObject.SetActive(isActiveByDefault);
            
            _pool.Enqueue(new PullElement(newElement));
            return newElement;
        }

        private bool HasFreeElement(out T element, Vector2 newSpawn)
        {
            var pullElement = _pool.FirstOrDefault(e => e.Used == false);
            if (pullElement != null)
            {
                pullElement.Used = true;
                element = pullElement.Element;

                element.transform.position = _isUpdateContainer ? newSpawn : (Vector2)_container.position;
                element.gameObject.SetActive(true);
                return true;
            }
            
            element = null;
            return false;
        }

        private class PullElement
        {
            public readonly T Element;

            public bool Used;

            public PullElement(T element)
            {
                Element = element;
            }
        }
    }
}