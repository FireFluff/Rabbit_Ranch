using System;
using UnityEngine;

public class CarrotScript : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tinder.OnAddCarrotToQueue?.Invoke(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
