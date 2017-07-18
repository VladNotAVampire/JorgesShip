using UnityEngine;

public static class World 
{
    public const float ONE = 0.16f;
    
    public static ShipController ship;

    public static Quaternion LookAngle(Vector3 forwards, Vector3 upwards)
    {
        return Quaternion.Euler(0, 0,  Mathf.Atan2(upwards.y - forwards.y, upwards.x - forwards.x) * Mathf.Rad2Deg - 90);
    }

    public static Vector3 GetMousePosition()
    {
        Vector3 point = Camera.main.ScreenPointToRay(Input.mousePosition).origin;

        return new Vector3(point.x, point.y);
    }
}
