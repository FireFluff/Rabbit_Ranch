using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class CarrotSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject carrotPrefab;
    private ObjectPool<GameObject> carrotPool;
    
    [SerializeField] private InputActionReference spawnCarrotAction;
    
    private float _backgroundWidth;


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
        
        // TODO: get screen size on awake, calculate center and edges -> enter random float for x in range for carrot spawn range
    }
    
    

    private void Start()
    {
        spawnCarrotAction.action.performed += SpawnCarrot;
    }

    private void OnDisable()
    {
        spawnCarrotAction.action.performed -= SpawnCarrot;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    
    public void SpawnCarrot()
    {
        GameObject carrot = carrotPool.Get();
    }

    public void SpawnCarrot(InputAction.CallbackContext context)
    {
        GameObject carrot = carrotPool.Get();
    }
}
