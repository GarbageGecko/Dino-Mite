using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector2 _currentTilePosition;
    private Camera _mainCamera;
    private GameManager _gridManager;
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
        _gridManager = FindObjectOfType<GameManager>();
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
            if (targetTile != null && targetTile.transform.position.y == 0)
            {
                float horizontalDifference = targetTile.transform.position.x - _currentTilePosition.x;
                if (Mathf.Abs(horizontalDifference) == 2)
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
        if (other.CompareTag("BabyDino"))
        {
            TouchBabyDino();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(5);
    }

    private void TouchBabyDino()
    {
        //Debug.Log("You've reached the Baby Dino!");
        BabyDino babyDino = FindObjectOfType<BabyDino>();
        if (babyDino != null)
        {
            babyDino.StartFollowingPlayer();
            FindObjectOfType<GameManager>().HasReachedBaby = true;
        }
    }
}
