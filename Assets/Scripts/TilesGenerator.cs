using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TilesGenerator : MonoBehaviour
{
    public static TilesGenerator Instance;
    
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private Tiles tilePrefab;

    [Space]
    
    [SerializeField, Range(2, 20)] private float moveSpeed;
    [SerializeField, Range(10, 20)] private int tilesToSpawn;
    [SerializeField, Range(1, 5)] private int maxTilesWithoutObstacles;
    
    private List<Tiles> _currentTiles = new List<Tiles>();

    private int _nextTileToActivate = -1;
    private Camera _camera;

    private void Awake()
    {
        Instance = this;

        _camera = Camera.main;
    }

    private void Start()
    {
        Vector3 spawnPos = startPoint.position;
        int tilesWithNoObstacles = maxTilesWithoutObstacles;

        for (int i = 0; i < tilesToSpawn; i++)
        {
            spawnPos -= tilePrefab.StartPoint.localPosition;
            
            Tiles tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity) as Tiles;

            if (tilesWithNoObstacles > 0)
            {
                tile.HideAllObstacles();
                tilesWithNoObstacles--;
            }
            else
            {
                tile.ActivateRandomObstacle(0);
            }

            spawnPos = tile.EndPoint.position;
            tile.transform.SetParent(transform);
            _currentTiles.Add(tile);
        }
    }

    private void Update()
    {
        if (Game.Instance.State == Game.GameStates.GameStarted)
        {
            Game.Instance.Score += (Time.deltaTime * moveSpeed);
        }

        if (_camera.WorldToViewportPoint(_currentTiles[0].EndPoint.position).z < 0)
        {
            Tiles tile = _currentTiles[0];
            _currentTiles.RemoveAt(0);
            tile.transform.position =
                _currentTiles[_currentTiles.Count - 1].EndPoint.position - tile.StartPoint.localPosition;
            
            tile.ActivateRandomObstacle((int)Game.Instance.Score);
            _currentTiles.Add(tile);
        }
    }
}
