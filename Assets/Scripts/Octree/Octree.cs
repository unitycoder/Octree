// based on https://github.com/CodingTrain/QuadTree

using System.Collections.Generic;
using UnityEngine;

public class Octree
{
    public Cube boundary;
    int capacity;
    List<Point> points;
    bool divided;

    Octree northEastBack;
    Octree northWestBack;
    Octree southEastBack;
    Octree southWestBack;
    Octree northEastFront;
    Octree northWestFront;
    Octree southEastFront;
    Octree southWestFront;

    public Octree(Cube _boundary, int _capacity)
    {
        boundary = _boundary;
        capacity = _capacity;
        points = new List<Point>();
        divided = false;
    }

    public bool Insert(Point point)
    {
        if (boundary.Contains(point) == false)
        {
            return false;
        }

        if (points.Count < capacity)
        {
            points.Add(point);
            return true;
        }
        else // need to divide
        {
            // if not already split, then divide
            if (divided == false)
            {
                Subdivide();
            }

            // check which child contains this point
            if (northEastBack.Insert(point) == true)
            {
                return true;
            }
            else if (northWestBack.Insert(point) == true)
            {
                return true;
            }
            else if (southEastBack.Insert(point) == true)
            {
                return true;
            }
            else if (southWestBack.Insert(point) == true)
            {
                return true;
            }

            else if (northEastFront.Insert(point) == true)
            {
                return true;
            }
            else if (northWestFront.Insert(point) == true)
            {
                return true;
            }
            else if (southEastFront.Insert(point) == true)
            {
                return true;
            }
            else if (southWestFront.Insert(point) == true)
            {
                return true;
            }
        }

        // error?
        Debug.Log("Cannot add point: " + point.ToString());
        Debug.DrawLine(new Vector3(0, 0, 0), point.Position(), Color.yellow, 5);
        return false;
    }

    void Subdivide()
    {
        var x = boundary.centerX;
        var y = boundary.centerY;
        var z = boundary.centerZ;
        var w = boundary.width;
        var h = boundary.height;
        var d = boundary.depth;

        var neb = new Cube(x + w / 2, y + h / 2, z + d / 2, w / 2, h / 2, d / 2);
        northEastBack = new Octree(neb, capacity);
        var nwb = new Cube(x - w / 2, y + h / 2, z + d / 2, w / 2, h / 2, d / 2);
        northWestBack = new Octree(nwb, capacity);
        var seb = new Cube(x + w / 2, y - h / 2, z + d / 2, w / 2, h / 2, d / 2);
        southEastBack = new Octree(seb, capacity);
        var swb = new Cube(x - w / 2, y - h / 2, z + d / 2, w / 2, h / 2, d / 2);
        southWestBack = new Octree(swb, capacity);

        var nef = new Cube(x + w / 2, y + h / 2, z - d / 2, w / 2, h / 2, d / 2);
        northEastFront = new Octree(nef, capacity);
        var nwf = new Cube(x - w / 2, y + h / 2, z - d / 2, w / 2, h / 2, d / 2);
        northWestFront = new Octree(nwf, capacity);
        var sef = new Cube(x + w / 2, y - h / 2, z - d / 2, w / 2, h / 2, d / 2);
        southEastFront = new Octree(sef, capacity);
        var swf = new Cube(x - w / 2, y - h / 2, z - d / 2, w / 2, h / 2, d / 2);
        southWestFront = new Octree(swf, capacity);

        divided = true;
    }

    public void DrawDebug()
    {
        var bottomLeftBack = new Vector3(boundary.centerX - boundary.width, boundary.centerY - boundary.height, boundary.centerZ + boundary.depth);
        var bottomLeftFront = new Vector3(boundary.centerX - boundary.width, boundary.centerY - boundary.height, boundary.centerZ - boundary.depth);
        var bottomRigthBack = new Vector3(boundary.centerX + boundary.width, boundary.centerY - boundary.height, boundary.centerZ + boundary.depth);
        var bottomRigthFront = new Vector3(boundary.centerX + boundary.width, boundary.centerY - boundary.height, boundary.centerZ - boundary.depth);

        var topLeftBack = new Vector3(boundary.centerX - boundary.width, boundary.centerY + boundary.height, boundary.centerZ + boundary.depth);
        var topLeftFront = new Vector3(boundary.centerX - boundary.width, boundary.centerY + boundary.height, boundary.centerZ - boundary.depth);
        var topRigthBack = new Vector3(boundary.centerX + boundary.width, boundary.centerY + boundary.height, boundary.centerZ + boundary.depth);
        var topRigthFront = new Vector3(boundary.centerX + boundary.width, boundary.centerY + boundary.height, boundary.centerZ - boundary.depth);

        Debug.DrawLine(bottomLeftBack, bottomLeftFront, Color.red);
        Debug.DrawLine(bottomRigthBack, bottomRigthFront, Color.green);
        Debug.DrawLine(bottomLeftBack, bottomRigthBack, Color.magenta);
        Debug.DrawLine(bottomLeftFront, bottomRigthFront, Color.gray);

        Debug.DrawLine(topLeftBack, topLeftFront, Color.yellow);
        Debug.DrawLine(topRigthBack, topRigthFront, Color.blue);
        Debug.DrawLine(topLeftBack, topRigthBack, Color.cyan);
        Debug.DrawLine(topLeftFront, topRigthFront, Color.white);

        Debug.DrawLine(bottomLeftBack, topLeftBack, Color.red);
        Debug.DrawLine(bottomLeftFront, topLeftFront, Color.green);
        Debug.DrawLine(bottomRigthBack, topRigthBack, Color.magenta);
        Debug.DrawLine(bottomRigthFront, topRigthFront, Color.gray);

        // recursively show children
        if (divided == true)
        {
            northEastBack.DrawDebug();
            northWestBack.DrawDebug();
            southEastBack.DrawDebug();
            southWestBack.DrawDebug();
            northEastFront.DrawDebug();
            northWestFront.DrawDebug();
            southEastFront.DrawDebug();
            southWestFront.DrawDebug();
        }

        // draw actual points
        for (int i = 0, length = points.Count; i < length; i++)
        {
            Debug.DrawRay(new Vector3(points[i].x, points[i].y, points[i].z), Vector3.up * 0.1f, Color.white);
            Debug.DrawRay(new Vector3(points[i].x, points[i].y, points[i].z), -Vector3.up * 0.1f, Color.white);

            Debug.DrawRay(new Vector3(points[i].x, points[i].y, points[i].z), Vector3.forward * 0.1f, Color.white);
            Debug.DrawRay(new Vector3(points[i].x, points[i].y, points[i].z), -Vector3.forward * 0.1f, Color.white);

            Debug.DrawRay(new Vector3(points[i].x, points[i].y, points[i].z), Vector3.right * 0.1f, Color.white);
            Debug.DrawRay(new Vector3(points[i].x, points[i].y, points[i].z), -Vector3.right * 0.1f, Color.white);
        }

    }
}
