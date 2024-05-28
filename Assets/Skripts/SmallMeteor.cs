using UnityEngine;
using System.Collections;
public class SmallMeteor : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    private bool _waitingForPlayerMove = false;

    public void Init(Rigidbody2D parentRigidbody)
    {
        _rigidbody = parentRigidbody;
    }

    void Update()
    {
        if (!_waitingForPlayerMove)
        {
            MoveDown();
        }
    }

    public void MoveDown()
    {
        if (_rigidbody != null)
        {
            _rigidbody.MovePosition(transform.position + new Vector3(0, -1, 0));
        }

        if (transform.position.y <= 0)
        {
            StartCoroutine(WaitForPlayerMoveThenDestroy());
        }
    }

    IEnumerator WaitForPlayerMoveThenDestroy()
    {
        _waitingForPlayerMove = true;
        yield return new WaitUntil(() => FindObjectOfType<Player>().HasMoved);
        Destroy(gameObject); // Den SmallMeteor zerstören, nachdem sich der Spieler bewegt hat
    }
}

