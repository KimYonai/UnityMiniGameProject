using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool returnPool;
    [SerializeField] GameObject objectPool;

    private void Start()
    {
        objectPool = GameObject.FindGameObjectWithTag("Enemy");
        returnPool = objectPool.GetComponent<ObjectPool>();
    }

    public void ReturnToPool()
    {
        if (returnPool != null)
        {
            returnPool.ReturnPool(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
