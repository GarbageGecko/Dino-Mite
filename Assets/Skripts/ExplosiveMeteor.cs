using UnityEngine;
// explosive Meteor skript

public class ExplosiveMeteor : Meteor
{
    [SerializeField] private SmallMeteor _smallMeteorPrefab;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }


    public override void MoveDown()
    {
        _rigidbody.MovePosition(transform.position + new Vector3(0, -2, 0)); // Bewegt den ExplosiveMeteor um ein Feld nach unten

        // Überprüfen, ob der ExplosiveMeteor ganz unten angekommen ist
        if (transform.position.y <= 0)
        {
            SpawnSmallMeteors();
            Destroy(gameObject); // Den ExplosiveMeteor zerstören
        }
    }

    private void SpawnSmallMeteors()
    {
        Vector3 leftSpawnPosition = transform.position + new Vector3(-2, 0, 0);
        Vector3 middleSpawnPosition = transform.position + new Vector3(0, 0, 0);
        Vector3 rightSpawnPosition = transform.position + new Vector3(2, 0, 0);

        // Überprüfung ob die Spawn-Positionen innerhalb des Grids liegen
        if (IsWithinGrid(leftSpawnPosition))
        {
            SmallMeteor leftSmallMeteor = Instantiate(_smallMeteorPrefab, leftSpawnPosition, Quaternion.identity);
            leftSmallMeteor.Init(_rigidbody);
        }

        if (IsWithinGrid(rightSpawnPosition))
        {
            SmallMeteor rightSmallMeteor = Instantiate(_smallMeteorPrefab, rightSpawnPosition, Quaternion.identity);
            rightSmallMeteor.Init(_rigidbody);
        }


        SmallMeteor middleSmallMeteor = Instantiate(_smallMeteorPrefab, middleSpawnPosition, Quaternion.identity);
        middleSmallMeteor.Init(_rigidbody);

    }

    private bool IsWithinGrid(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);

        return x >= 0 && x < _gameManager.Width && y >= 0 && y < _gameManager.Height;
    }

}


