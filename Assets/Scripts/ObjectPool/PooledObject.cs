using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool returnPool;

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
