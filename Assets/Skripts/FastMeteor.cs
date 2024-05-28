using UnityEngine;
public class FastMeteor : Meteor
{
    public override void MoveDown()
    {
        _rigidbody.MovePosition(transform.position + new Vector3(0, -4, 0)); // Bewegt den Meteor um zwei Felder nach unten
    }
}
