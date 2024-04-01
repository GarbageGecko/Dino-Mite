using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    public void MoveDown()
    {
        _rigidbody.MovePosition(transform.position + new Vector3(0, -1, 0));
    }
}
