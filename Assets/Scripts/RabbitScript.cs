using System.Drawing;
using UnityEngine;

public class RabbitScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 2f;
    private Vector2 _currentPosition;
    private Vector2 _targetPosition;
    
    void Start()
    {
        _currentPosition = transform.position;
        Tinder.OnAddRabbitToQueue?.Invoke(gameObject);
    }

    public void EatCarrot(GameObject carrot)
    {
        _targetPosition = carrot.transform.position;
        Vector2 direction = (_targetPosition - _currentPosition).normalized;
        if (Vector2.Distance(_currentPosition, _targetPosition) < 0.1f)
        {
            Destroy(carrot);
        }
        else
        {
            rb.linearVelocity = direction * moveSpeed;
        }
    }
}
