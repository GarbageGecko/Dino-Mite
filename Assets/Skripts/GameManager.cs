using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance

    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Meteor _meteorPrefab;
    [SerializeField] private Meteor _fastMeteorPrefab;
    [SerializeField] private Meteor _explosiveMeteorPrefab;
    [SerializeField] private SmallMeteor _smallMeteorPrefab;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private BabyDino _babyDinoPrefab;
    [SerializeField] private Transform _cam;
    private List<GameObject> _meteorPreviews = new List<GameObject>();
    [SerializeField] private GameObject _meteorPreviewPrefab;

    private Dictionary<Vector2, Tile> _tiles;
    private List<Meteor> _meteors = new List<Meteor>();
    private Player _player;
    private float _nextSpawnTime = 0f;
    private float _spawnInterval = 1f;
    private bool _hasReachedBaby = false;
    static public string _activeSceneName;

    public int Width => _width;
    public int Height => _height;
    public bool HasReachedBaby { get => _hasReachedBaby; set => _hasReachedBaby = value; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GenerateGrid();
        StartRound();
        _activeSceneName = SceneManager.GetActiveScene().name;
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x += 2)
        {
            for (int y = 0; y < _height; y += 2)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile: {x} {y}";
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset, y); // Pass the y-coordinate (height)
                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        //_cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
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
        Instantiate(_babyDinoPrefab, tile.transform.position, Quaternion.identity);
    }

    void Update()
    {
        HandlePlayerInput();

        if (_hasReachedBaby && _player.CurrentTilePosition == new Vector2(0, 0))
        {
            SceneManager.LoadScene(6);
            _hasReachedBaby = false;
        }
        DisplayMeteorPreviews();
    }

    public void OnTileClicked(Tile tile)
    {
        if (_player != null && tile.transform.position.y == 0)
        {
            Vector2 targetPosition = tile.Position;
            if (Vector2.Distance(targetPosition, _player.CurrentTilePosition) == 2) // Ensuring only 1 tile move horizontally
            {
                _player.MoveToTile(tile);
            }
        }
    }

    void DisplayMeteorPreviews()
    {
        ClearMeteorPreviews(); // Vorherige Vorschau löschen

        foreach (var meteor in _meteors)
        {
            // Standard-Vorschau für normale und FastMeteor-Meteoriten
            Vector2 nextPosition = GetNextMeteorPosition(meteor);
            GameObject previewObject = Instantiate(_meteorPreviewPrefab, nextPosition, Quaternion.identity);
            _meteorPreviews.Add(previewObject);

            // Überprüfen, ob es sich um einen explosiven Meteor handelt
            if (meteor.GetType() == typeof(ExplosiveMeteor))
            {
                ExplosiveMeteor explosiveMeteor = (ExplosiveMeteor)meteor;

                // Überprüfen, ob der Explosivmeteor ganz unten ist
                if (Mathf.Approximately(explosiveMeteor.transform.position.y, 0))
                {
                    List<Vector2> smallMeteorPositions = GetSmallMeteorPositions(explosiveMeteor);
                    foreach (var smallMeteorPosition in smallMeteorPositions)
                    {
                        // Vorschau für kleine Meteoriten anzeigen
                        GameObject smallMeteorPreview = Instantiate(_meteorPreviewPrefab, smallMeteorPosition, Quaternion.identity);
                        _meteorPreviews.Add(smallMeteorPreview);
                    }
                }
            }
        }
    }

    // Berechnet die Positionen der kleinen Meteoriten, die entstehen, wenn ein Explosiver Meteor auftrifft
    List<Vector2> GetSmallMeteorPositions(ExplosiveMeteor explosiveMeteor)
    {
        List<Vector2> smallMeteorPositions = new List<Vector2>();

        Vector2 explosivePosition = explosiveMeteor.transform.position;
        smallMeteorPositions.Add(explosivePosition + new Vector2(-2, 0)); // Links vom Explosiven Meteor
        smallMeteorPositions.Add(explosivePosition + new Vector2(2, 0)); // Rechts vom Explosiven Meteor

        return smallMeteorPositions;
    }

    // Berechnet die nächste Position eines Meteoriten basierend auf seiner aktuellen Position und Typ
    Vector2 GetNextMeteorPosition(Meteor meteor)
    {
        Vector2 currentPosition = meteor.transform.position;
        float moveDistance = 2; // Standard-Bewegungsentfernung

        // Überprüfen, ob es sich um einen FastMeteor handelt
        if (meteor.GetType() == typeof(FastMeteor))
        {
            moveDistance = 4; // Bei FastMeteor ist die Bewegungsdistanz verdoppelt
        }

        Vector2 nextPosition = currentPosition + new Vector2(0, -moveDistance);
        return nextPosition;
    }

    // Löscht die Vorschau-GameObjecte der Meteoritenbewegung
    void ClearMeteorPreviews()
    {
        foreach (var preview in _meteorPreviews)
        {
            Destroy(preview);
        }
        _meteorPreviews.Clear();
    }

    void HandlePlayerInput()
    {
        if (_player != null)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (horizontalInput != 0)
            {
                Vector2 targetPosition = _player.CurrentTilePosition + new Vector2(horizontalInput * 2, 0);
                var targetTile = GetTileAtPosition(targetPosition);
                if (targetTile != null && targetTile.transform.position.y == 0)
                {
                    _player.MoveToTile(targetTile);
                }
            }
        }
    }

    IEnumerator MoveMeteorsDownCoroutine()
    {
       // Debug.Log("MoveMeteorsDownCoroutine started.");
        while (true)
        {
            yield return new WaitUntil(() => _player.HasMoved);

            SpawnMeteorInRandomRow();

            foreach (var meteor in _meteors)
            {
                meteor.MoveDown();
            }

            CheckAndRemoveMeteorsAtBottom();

            _player.HasMoved = false;

            yield return null; // Jetzt nicht mehr warten, sondern direkt fortfahren
        }
    }

    public void SpawnMeteorInRandomRow()
    {
       // Debug.Log("SpawnMeteorInRandomRow started");
        int randomX = UnityEngine.Random.Range(0, _width / 2) * 2; // Stelle sicher, dass die zufällige X-Position gerade ist

        // Prüfen, ob die aktuelle Spalte nicht die letzte Spalte ist
        if (randomX != _width - 1)
        {
            var tile = GetTileAtPosition(new Vector2(randomX, _height - 2));
            if (tile != null)
            {
                Meteor newMeteor;

                switch (_activeSceneName)
                {
                    case "Level1":
                       // Debug.Log("Level 1 Meteoriten wurden gestartet");
                        newMeteor = Instantiate(_meteorPrefab, tile.transform.position, Quaternion.identity);
                        break;
                    case "Level2":
                        bool spawnFastMeteor = UnityEngine.Random.Range(0, 2) == 0;
                        newMeteor = spawnFastMeteor ? Instantiate(_fastMeteorPrefab, tile.transform.position, Quaternion.identity) : Instantiate(_meteorPrefab, tile.transform.position, Quaternion.identity);
                        break;
                    case "Level3":
                        bool spawnExplosiveMeteor = UnityEngine.Random.Range(0, 10) < 3;
                        newMeteor = spawnExplosiveMeteor ? Instantiate(_explosiveMeteorPrefab, tile.transform.position, Quaternion.identity) : UnityEngine.Random.Range(0, 2) == 0 ? Instantiate(_fastMeteorPrefab, tile.transform.position, Quaternion.identity) : Instantiate(_meteorPrefab, tile.transform.position, Quaternion.identity);
                        break;
                    default:
                        newMeteor = Instantiate(_meteorPrefab, tile.transform.position, Quaternion.identity);
                        break;
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
        return _tiles.TryGetValue(pos, out var tile) ? tile : null;
    }
}
