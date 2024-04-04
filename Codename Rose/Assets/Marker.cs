using System;
using _Scripts.PlayerScripts;
using UnityEngine;

public class Marker : MonoBehaviour
{
    private Vector2 _markedLocation;
    private Vector2 _heroPosition;

    private void Awake()
    {
        _heroPosition = FindObjectOfType<Hero>().transform.position;
    }

    public void SetMarkedLocation(Vector2 location)
    {
        _markedLocation = location;
    }

    private void Update()
    {
        transform.position = new Vector2(_markedLocation.x,_markedLocation.y);
    }
}