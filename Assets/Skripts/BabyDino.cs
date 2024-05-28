using UnityEngine;

public class BabyDino : MonoBehaviour
{
    private Player _player;
    private bool _isFollowing = false;

    void Start()
    {
        GameManager gridManager = FindObjectOfType<GameManager>();
        Vector2 dinoPosition = new Vector2(gridManager.Width - 1, 0);
        Tile tile = gridManager.GetTileAtPosition(dinoPosition);
        if (tile != null)
        {
            transform.position = tile.transform.position;
        }

        _player = FindObjectOfType<Player>();
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

     public void DestroyBabyDino()
    {
        Destroy(gameObject);
    }
}
