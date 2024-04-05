using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Meteor _meteorPrefab;
    [SerializeField] private Meteor _fastMeteorPrefab;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private BabyDino _babyDinoPrefab;
    [SerializeField] private Transform _cam;



    private Dictionary<Vector2, Tile> _tiles;
    private List<Meteor> _meteors = new List<Meteor>();
    private Player _player;
    private float _nextSpawnTime = 0f;
    private float _spawnInterval = 1f;
    private bool _hasReachedBaby = false;

    public int Width => _width;
    public int Height => _height;
    public bool HasReachedBaby
    {
        get { return _hasReachedBaby; }
        set { _hasReachedBaby = value; }
    }

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
        SpawnBabyDino();
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

    void SpawnBabyDino()
    {
        var tile = GetTileAtPosition(new Vector2(_width - 1, 0));
        if (tile != null)
        {
            Instantiate(_babyDinoPrefab, tile.transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        HandlePlayerInput();

        if (_hasReachedBaby && _player.CurrentTilePosition == new Vector2(0, 0))
        {
            Debug.Log("You won!");
            _hasReachedBaby = false; // Setze den Status zurück
        }
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
                if (targetTile != null && targetTile.transform.position.y == 0 && Mathf.Abs(horizontalInput) == 1)
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
            Meteor newMeteor;

            if (SceneManager.GetActiveScene().name == "Level1")
            {
                newMeteor = Instantiate(_meteorPrefab, tile.transform.position, Quaternion.identity);
                _meteors.Add(newMeteor);
                _nextSpawnTime = Time.time + _spawnInterval;
            }
            else if (SceneManager.GetActiveScene().name == "Level2")
            {
                // Zufällige Auswahl zwischen normalem Meteor und FastMeteor
                bool spawnFastMeteor = UnityEngine.Random.Range(0, 2) == 0; // 50% Chance für true
                if (spawnFastMeteor)
                {
                    newMeteor = Instantiate(_fastMeteorPrefab, tile.transform.position, Quaternion.identity);
                }
                else
                {
                    newMeteor = Instantiate(_meteorPrefab, tile.transform.position, Quaternion.identity);
                }
                _meteors.Add(newMeteor);
                _nextSpawnTime = Time.time + _spawnInterval;
            }
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
