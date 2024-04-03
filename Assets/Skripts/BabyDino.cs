using UnityEngine;

public class BabyDino : MonoBehaviour
{
    private Player _player; // Referenz zum Spieler
    private bool _isFollowing = false; // Flag, um zu überprüfen, ob das Baby Dino dem Spieler folgt

    void Start()
    {
        GridManager gridManager = FindObjectOfType<GridManager>();
        Vector2 dinoPosition = new Vector2(gridManager.Width - 1, 0);
        Tile tile = gridManager.GetTileAtPosition(dinoPosition);
        if (tile != null)
        {
            transform.position = tile.transform.position;
        }

        _player = FindObjectOfType<Player>(); // Spieler-Referenz erhalten
    }

    void Update()
    {
        if (_isFollowing)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        if (_player != null)
        {
            // Den Baby Dino in die Position des Spielers bewegen
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Time.deltaTime * 2f);
        }
    }

    public void StartFollowingPlayer()
    {
        _isFollowing = true;
    }
}
