using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaikin : MonoBehaviour
{
    public int iterations;

    // draw gizmo sphere
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Vector3 p1 = new Vector3(0, 0, 0);
        Vector3 p2 = new Vector3(1, 1, 0);
        Vector3 p3 = new Vector3(2, 1, 0);
        Vector3 p4 = new Vector3(3, 0, 0);

        List<Vector3> vertices = new List<Vector3>();

        vertices.Add(p1);
        vertices.Add(p2);
        vertices.Add(p3);
        vertices.Add(p4);


        List<Vector3> temp = new List<Vector3>();
        for (int j = 0; j < iterations; j++)
        {
            temp = new List<Vector3>();

            temp.Add(p1);
            for (int i = 0; i < vertices.Count; i++)
            {
                if (i < vertices.Count - 1)
                {
                    temp.Add(Q(vertices[i], vertices[i + 1]));
                    temp.Add(R(vertices[i], vertices[i + 1]));
                }
            }
            temp.Add(p4);
            vertices = temp;
        }



        for (int i = 0; i < vertices.Count; i++)
        {
            Gizmos.DrawLine(vertices[i], vertices[i + 1]);
        }
    }
    private Vector3 Q(Vector3 p1, Vector3 p2)
    {
        return new Vector3(0.75f * p1.x + 0.25f * p2.x, 0.75f * p1.y + 0.25f * p2.y, 0.75f * p1.z + 0.25f * p2.z);
    }

    private Vector3 R(Vector3 p1, Vector3 p2)
    {
        return new Vector3(0.25f * p1.x + 0.75f * p2.x, 0.25f * p1.y + 0.75f * p2.y, 0.25f * p1.z + 0.75f * p2.z);
    }
}
