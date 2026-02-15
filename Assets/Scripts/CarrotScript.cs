using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarrotScript : MonoBehaviour
{
    [SerializeField] private BoxCollider2D carrotCollider;

    private bool hasCalledForEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carrotCollider.offset = new Vector2(0, Random.Range(-5f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain") && !hasCalledForEvent)
        {
            Tinder.OnAddCarrotToQueue?.Invoke(this.gameObject);
            hasCalledForEvent = true;
        }
    }
}