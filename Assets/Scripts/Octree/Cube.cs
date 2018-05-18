public class Cube
{
    public float centerX;
    public float centerY;
    public float centerZ;
    public float width;
    public float height;
    public float depth;

    public Cube(float x, float y, float z, float w, float h, float d)
    {
        this.centerX = x;
        this.centerY = y;
        this.centerZ = z;
        this.width = w;
        this.height = h;
        this.depth = d;
    }

    public bool Contains(Point point)
    {
        bool contains = (point.x > centerX - width && point.x < centerX + width && point.y > centerY - height && point.y < centerY + height && point.z > centerZ - depth && point.z < centerZ + depth);
        return contains;
    }
}
