using UnityEngine;
//fast Meteor skript
public class FastMeteor : Meteor
{
    public override void MoveDown()
    {
        _rigidbody.MovePosition(transform.position + new Vector3(0, -2, 0)); // Bewegt den Meteor um zwei Felder nach unten
    }
}
