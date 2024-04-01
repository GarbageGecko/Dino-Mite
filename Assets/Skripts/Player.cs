using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector2 _currentTilePosition;
    private float _moveSpeed = 1f;
    private Camera _mainCamera;
    private GridManager _gridManager;
    private bool _hasMoved = false;

    public Vector2 CurrentTilePosition => _currentTilePosition;
    public bool HasMoved
    {
        get { return _hasMoved; }
        set { _hasMoved = value; }
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _gridManager = FindObjectOfType<GridManager>();
    }

    private void Update()
    {
        HandlePlayerInput();
    }
   private void HandlePlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPosition = new Vector2(Mathf.Round(mouseWorldPosition.x), Mathf.Round(mouseWorldPosition.y));

            Tile targetTile = _gridManager.GetTileAtPosition(targetPosition);
            if (targetTile != null && targetTile.transform.position.y == 0) // Überprüfung, ob sich der Spieler in der untersten Zeile befindet
            {
                float horizontalDifference = targetTile.transform.position.x - _currentTilePosition.x;
                if (Mathf.Abs(horizontalDifference) == 1) // Überprüfung, ob sich der Spieler nur um ein Feld bewegt
                {
                    MoveToTile(targetTile);
                }
            }
        }
    }

    public void MoveToTile(Tile targetTile)
    {
        _currentTilePosition = new Vector2(targetTile.transform.position.x, targetTile.transform.position.y);
        transform.position = targetTile.transform.position;
        _hasMoved = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Meteor"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
