using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    // 使用单例模式，私有化方法避免外部创建
    public static ObjectsPool Instance { get; private set; }

    // 需要对象池的预制件
    [SerializeField] private GameObject prefab;

    // 对象池
    private Queue<GameObject> pooledInstanceQueue = new Queue<GameObject>();

    // 继承MonoBehaviour，由程序在第一时间创建
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject GetInstance()
    {
        if (pooledInstanceQueue.Count > 0)
        {
            GameObject instanceToReuse = pooledInstanceQueue.Dequeue();
            instanceToReuse.SetActive(true);
            return instanceToReuse;
        }

        return Instantiate(prefab);
    }

    public void ReturnInstance(GameObject gameObjectToPool)
    {
        pooledInstanceQueue.Enqueue(gameObjectToPool);
        gameObjectToPool.SetActive(false);
        gameObjectToPool.transform.SetParent(gameObject.transform);
    }
}