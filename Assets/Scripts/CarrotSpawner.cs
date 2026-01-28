using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class CarrotSpawner : MonoBehaviour
{
    // Serialized fields - Public -> Private
    [SerializeField] private GameObject carrotPrefab;
    [SerializeField] private InputActionReference spawnCarrotAction;
    
    // NonSerialized - Public -> Private
    private ObjectPool<GameObject> _carrotPool;
    private float _gameWidth;


    private void Awake()
    {
        _carrotPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(carrotPrefab, transform, true),
            actionOnGet: (carrot) => carrot.SetActive(true),
            actionOnRelease: (carrot) => carrot.SetActive(false),
            actionOnDestroy: (carrot) => Destroy(carrot),
            collectionCheck: false,
            defaultCapacity: 5,
            maxSize: 50
        );
        
        _gameWidth = Screen.width;
    }
    
    

    private void Start()
    {
        spawnCarrotAction.action.performed += SpawnCarrot;
    }

    private void OnDisable()
    {
        spawnCarrotAction.action.performed -= SpawnCarrot;
    }
    
    public void SpawnCarrot()
    {
        var carrot = _carrotPool.Get();
        float randomX = UnityEngine.Random.Range(0f, _gameWidth);
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(randomX, Screen.height, 10f));
        carrot.transform.position = spawnPosition;
    }

    private void SpawnCarrot(InputAction.CallbackContext context)
    {
        SpawnCarrot();
    }
}
