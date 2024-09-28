using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] PooledObject prefab;
    [SerializeField] int size;
    [SerializeField] int capacity;

    private Stack<PooledObject> bulletPool;

    private void Awake()
    {
        bulletPool = new Stack<PooledObject>(capacity);
        for (int i = 0; i < size; i++)
        {
            PooledObject instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            instance.returnPool = this;
            bulletPool.Push(instance);
        }
    }

    public PooledObject GetPool(Vector3 position, Quaternion rotation)
    {
        if (bulletPool.Count > 0)
        {
            PooledObject instance = bulletPool.Pop();
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            return null;
        }
    }

    public void ReturnPool(PooledObject instance)
    {
        if (bulletPool.Count < capacity)
        {
            instance.gameObject.SetActive(false);
            bulletPool.Push(instance);
        }
        else
        {
            Destroy(instance.gameObject);
        }
    }
}
