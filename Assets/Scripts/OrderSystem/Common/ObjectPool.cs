using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T: MonoBehaviour
{
    private string poolName=null;
    public string PoolName
    {
        get { return poolName; }
    }
    private GameObject prefab = null;
    private IList<GameObject> pool = null;
    public ObjectPool(GameObject prefab, string poolName="Pool" )
    {
        this.poolName = poolName;
        this.prefab = prefab;
        this.pool = new List<GameObject>();
    }
    public IList<T> Pop(int count)
    {
        IList<T> result=new List<T>();
        for (int i = 0; i < count; i++)
        {
            result.Add(Pop());

        }
        return result;
    }
    public T Pop()
    {
        if (pool.Count>0)
        {
            var result = pool[0];
            pool.RemoveAt(0);
            return result.GetComponent<T>();
        }
        return Create();
    }
    public void Push(GameObject go)
    {
        go.SetActive(false);
        pool.Add(go);
    }
    public void Push(T t)
    {
        Push(t.gameObject);
    }
    private T Create()
    {
        var obj = UnityEngine.Object.Instantiate(prefab);
        return obj.AddComponent<T>();
    }
}
