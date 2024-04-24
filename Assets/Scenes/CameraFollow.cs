using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private const float Threshold = 0.006f;
    private GameObject _player;
    private int _windowWidth;
    private int _windowHeight;
    private float _thresholdX;
    private float _thresholdY;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _windowHeight = Screen.height;
        _windowWidth = Screen.width;

        _thresholdX = _windowWidth * Threshold;
        _thresholdY = _windowHeight * Threshold;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        var cameraX = position.x;
        var cameraY = position.y;

        var playerPosition = _player.transform.position;

        var distanceFromPlayerX = playerPosition.x - cameraX;
        var distanceFromPlayerY = playerPosition.y - cameraY;


        var finalOffsetX = Math.Max(Math.Abs(distanceFromPlayerX) - _thresholdX, 0) * Math.Sign(distanceFromPlayerX);
        var finalOffsetY = Math.Max(Math.Abs(distanceFromPlayerY) - _thresholdY, 0) * Math.Sign(distanceFromPlayerY);

        transform.position = new Vector3(cameraX + finalOffsetX, cameraY + finalOffsetY, -1);
    }
}