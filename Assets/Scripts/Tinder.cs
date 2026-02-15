using System;
using System.Collections.Generic;
using UnityEngine;

public class Tinder : MonoBehaviour
{
    // Carrot queue
    public static Action<GameObject> OnAddCarrotToQueue;
    private Queue<GameObject> _carrotQueue = new Queue<GameObject>();
    
    // Rabbit queue
    public static Action<GameObject> OnAddRabbitToQueue;
    private Queue<GameObject> _rabbitQueue = new Queue<GameObject>();

    private void Start()
    {
		// Subscribe to the events
        OnAddCarrotToQueue += AddToCarrotQueue;
        OnAddRabbitToQueue += AddToRabbitQueue;
    }

    private void AddToCarrotQueue(GameObject carrot)
    {
        Debug.Log("Adding carrot to queue");
        _carrotQueue.Enqueue(carrot);
        var rabbitScript = LookForMatch("Rabbit")?.GetComponent<RabbitScript>();
        if (rabbitScript is not null) Match(rabbitScript, carrot);
    }
    
    private void AddToRabbitQueue(GameObject rabbit)
    {
        Debug.Log("Adding rabbit to queue");
        _rabbitQueue.Enqueue(rabbit);
        var carrot = LookForMatch("Carrot");
        var rabbitScript = rabbit?.GetComponent<RabbitScript>();
        if (carrot is not null) Match(rabbitScript, carrot);
        
    }
    
    private GameObject LookForMatch(string objectLookingFor)
    {
        if (_carrotQueue.Count <= 0 && _rabbitQueue.Count <= 0) return null;
        
        switch (objectLookingFor)
        {
            case "Rabbit":
            {
                if (_rabbitQueue.Count <= 0) return null;
                var rabbit = _rabbitQueue.Dequeue();
                return rabbit;
            }
            case "Carrot":
            {
                if (_carrotQueue.Count <= 0) return null;
                var carrot = _carrotQueue.Dequeue();
                return carrot;
            }
            default:
                throw new Exception("Invalid object type looking for a match");
        }
    }

    private static void Match(RabbitScript rabbit, GameObject carrot)
    {
        rabbit.EatCarrot(carrot);
    }
}
