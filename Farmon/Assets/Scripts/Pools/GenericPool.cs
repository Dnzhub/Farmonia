using System.Collections.Generic;
using UnityEngine;

namespace GrassMan.Pools
{
    public abstract class GenericPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] T[] prefabs;
        //Ýf not enough object in pool it will add object in pool as poolSizeOfIncrement
        [SerializeField] int poolSizeOfIncrement;

        Queue<T> _poolPrefabs = new Queue<T>();
        int index = 0;

        private void Awake()
        {
            SingletonObject();

        }
        private void OnEnable()
        {
            GrowPool();
            DefaultIncrementSize();
        }

        protected abstract void SingletonObject();

        private void DefaultIncrementSize()
        {
            if (poolSizeOfIncrement == 0)
            {
                poolSizeOfIncrement = 5;
            }
        }

        public T Get()
        {
            if (_poolPrefabs.Count == 0) //If there is not enough space on pool grow it
            {
                GrowPool();
            }

            return _poolPrefabs.Dequeue();
        }

        private void GrowPool()
        {
            for (int i = 0; i < poolSizeOfIncrement; i++)
            {
                T newPrefab = Instantiate(prefabs[index]);
                newPrefab.transform.parent = this.transform;
                newPrefab.gameObject.SetActive(false);
                _poolPrefabs.Enqueue(newPrefab);
                index++;

                if (index >= prefabs.Length)
                {
                    index = 0;
                }
            }
        }
        public void SetBack(T poolObject)
        {
            poolObject.gameObject.SetActive(false);
            _poolPrefabs.Enqueue(poolObject);
        }
    }

}
