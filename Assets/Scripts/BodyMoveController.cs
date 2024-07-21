using UnityEngine;

public class BodyMoveController : BodyPart
{
    public void SetDirection(Vector3 point)
    {
        Vector3 vec = point - transform.position;
        direction = new Vector2(Mathf.Round(vec.x), Mathf.Round(vec.y));
    }
}
