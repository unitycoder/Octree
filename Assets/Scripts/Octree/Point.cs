using UnityEngine;

public class Point
{
    public float x;
    public float y;
    public float z;

    public Point(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override string ToString()
    {
        return x + "," + y + "," + z;
    }

    public Vector3 Position()
    {
        return new Vector3(x, y, z);
    }
}
