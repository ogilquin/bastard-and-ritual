using UnityEngine;
using System.Collections;

public static class Utils 
{
    // Retourn un vector3 ou y = z, permet de gerer le depth.
	public static Vector3 PositionDepth(Vector3 position)
    {
        position.z = position.y;
        return position;
    }

    public static Vector3 PositionDepth(Vector2 position)
    {
        return new Vector3(position.x, position.y, position.y);
    }
    public static Vector2 RoundPosition(Vector2 position)
    {
        return new Vector2(Mathf.Round(position.x), Mathf.Round(position.y));
    }
}
