// unity octree https://github.com/unitycoder/Octree

using UnityEngine;

public class Main : MonoBehaviour
{
    Octree octTree;

    void Start()
    {
        var areaCenter = new Vector3(0, 0, 0);
        float width = 10;
        float height = 10;
        float depth = 10;

        Cube boundary = new Cube(areaCenter.x, areaCenter.y, areaCenter.z, width / 2, height / 2, depth / 2);

        int capacity = 4;
        octTree = new Octree(boundary, capacity);
    }

    void Update()
    {
        // show current octree
        octTree.DrawDebug();

        // press mouse to insert random point
        if (Input.GetMouseButtonDown(0))
        {
            var b = octTree.boundary;
            var pos = new Vector3(Random.Range(b.centerX - b.width, b.centerX + b.width), Random.Range(b.centerY - b.height, b.centerY + b.height), Random.Range(b.centerZ - b.depth, b.centerZ + b.depth));
            var p = new Point(pos.x, pos.y, pos.z);
            octTree.Insert(p);
        }
    }

}
