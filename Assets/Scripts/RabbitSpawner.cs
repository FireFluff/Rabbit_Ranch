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

    void Start()
    {
        var rabbit = _rabbitPool.Get();
        float randomX = UnityEngine.Random.Range(0f, _gameWidth);
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(randomX, 0f, 10f));
        rabbit.transform.position = spawnPosition;
    }
    
    private static void OnGet(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}
