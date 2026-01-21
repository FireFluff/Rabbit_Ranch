using System;
using UnityEngine;
using UnityEngine.Pool;

public class CarrotSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject carrotPrefab;
    private ObjectPool<GameObject> carrotPool;


    private void Awake()
    {
        carrotPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(carrotPrefab),
            actionOnGet: (carrot) => carrot.SetActive(true),
            actionOnRelease: (carrot) => carrot.SetActive(false),
            actionOnDestroy: (carrot) => Destroy(carrot),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 50
        );
    }
    
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SpawnCarrot()
    {
        GameObject carrot = carrotPool.Get();
    }
}
