using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class RabbitSpawner : MonoBehaviour
{
    // Serialized fields - Public on top, Private on bottom
    [SerializeField] private GameObject rabbitPrefab;
    
    // NonSerialized - Public on top, Private on bottom
    private ObjectPool<GameObject> _rabbitPool;
    private float _gameWidth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rabbitPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(rabbitPrefab, transform, true),
            actionOnGet: OnGet,
            actionOnRelease: (rabbit) => rabbit.SetActive(false),
            actionOnDestroy: Destroy,
            collectionCheck: false,
            defaultCapacity: 5,
            maxSize: 50
        );
        
        _gameWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private static void OnGet(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}
