using UnityEngine;
//meteor skript

public class Meteor : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rigidbody;

    public virtual void MoveDown()
    {
        _rigidbody.MovePosition(transform.position + new Vector3(0, -2, 0));
    }
}
