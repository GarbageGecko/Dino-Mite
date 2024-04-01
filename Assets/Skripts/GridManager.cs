using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Meteor _meteorPrefab;
    [SerializeField] private Player _playerPrefab; 
    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;
    private List<Meteor> _meteors = new List<Meteor>();
    private Player _player;
    private float _nextSpawnTime = 0f;
    private float _spawnInterval = 1f;

    void Start()
    {
        GenerateGrid();
        StartRound();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile: {x} {y}";
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    void StartRound()
    {
        SpawnPlayer();
        StartCoroutine(MoveMeteorsDownCoroutine());
    }

    void SpawnPlayer()
    {
        var tile = GetTileAtPosition(new Vector2(0, 0));
        if (tile != null)
        {
            _player = Instantiate(_playerPrefab, tile.transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        HandlePlayerInput();
    }

   void HandlePlayerInput()
    {
        if (_player != null)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (horizontalInput != 0)
            {
                Vector2 targetPosition = _player.CurrentTilePosition + new Vector2(horizontalInput, 0);
                var targetTile = GetTileAtPosition(targetPosition);
                if (targetTile != null && targetTile.transform.position.y == 0 && Mathf.Abs(horizontalInput) == 1) // Überprüfung, ob sich der Spieler in der untersten Zeile befindet und nur um ein Feld bewegt
                {
                    _player.MoveToTile(targetTile);
                }
            }
        }
    }

    IEnumerator MoveMeteorsDownCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => _player.HasMoved);

            if (Time.time >= _nextSpawnTime)
            {
                SpawnMeteorInRandomRow();
            }

            foreach (var meteor in _meteors)
            {
                meteor.MoveDown();
            }

            CheckAndRemoveMeteorsAtBottom();

            _player.HasMoved = false;

            yield return new WaitForSeconds(1f); 
        }
    }

    void SpawnMeteorInRandomRow()
    {
        int randomX = UnityEngine.Random.Range(0, _width);
        var tile = GetTileAtPosition(new Vector2(randomX, _height - 1));
        if (tile != null)
        {
            var newMeteor = Instantiate(_meteorPrefab, tile.transform.position, Quaternion.identity);
            _meteors.Add(newMeteor);
            _nextSpawnTime = Time.time + _spawnInterval;
        }
    }

    void CheckAndRemoveMeteorsAtBottom()
    {
        List<Meteor> meteorsToRemove = new List<Meteor>();
        foreach (var meteor in _meteors)
        {
            if (meteor.transform.position.y <= 0)
            {
                meteorsToRemove.Add(meteor);
                Destroy(meteor.gameObject);
            }
        }

        foreach (var meteor in meteorsToRemove)
        {
            _meteors.Remove(meteor);
        }
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
}
