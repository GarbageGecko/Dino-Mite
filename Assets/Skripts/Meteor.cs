using UnityEngine;
//meteor skript

public class Meteor : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rigidbody;

    public virtual void MoveDown()
    {
        _rigidbody.MovePosition(transform.position + new Vector3(0, -2, 0));
    }
    public virtual Vector3 GetNextPosition()
{
    return transform.position + new Vector3(0, -2, 0); // Standardbewegung um 2 Einheiten nach unten
}

}
