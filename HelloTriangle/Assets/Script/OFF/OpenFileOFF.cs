using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class OpenFileOFF : MonoBehaviour
{
    public string file;

    private string filePath;

    void Start()
    {
        filePath = "Assets\\Script\\OFF\\" + file;
        OffReader offReader = new OffReader(filePath);

        Mesh msh = new Mesh();

        msh.vertices = offReader.getVertices();
        msh.triangles = offReader.getTriangles();

        gameObject.GetComponent<MeshFilter>().mesh = msh;
    }
}

class OffReader
{
    private string filePath;
    private int numberOfVertices;
    private int numberOfTriangles;
    private string[] lineTab;

    public OffReader(string filePath)
    {
        this.filePath = filePath;
        init();
    }

    private void init()
    {
        var lines = File.ReadAllLines(filePath).Select(s => s.Trim());
        lineTab = lines.ToArray();

        if (lineTab[0] != "OFF")
            throw new Exception("You are not reading a OFF file");

        this.numberOfVertices = int.Parse(lineTab[1].Split(' ')[0]);
        this.numberOfTriangles = int.Parse(lineTab[1].Split(' ')[1]);
    }

    public Vector3[] getVertices()
    {
        Vector3[] vertices = new Vector3[numberOfVertices];
        for (int i = 0; i < numberOfVertices; i++)
        {
            float[] vertex = lineTab[i + 2].Split(' ').Select(s => float.Parse(s)).ToArray();
            vertices[i] = new Vector3(vertex[0], vertex[1], vertex[2]);

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = vertices[i];
            sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        return vertices;
    }

    public int[] getTriangles()
    {
        int[] triangles = new int[numberOfTriangles*3];
        for (int i = 0; i < numberOfTriangles; i++)
        {
            int[] vertex = lineTab[i + 2 + numberOfVertices].Split(' ').Select(s => int.Parse(s)).ToArray();
            triangles[i*3] = vertex[1];
            triangles[i*3+1] = vertex[2];
            triangles[i*3+2] = vertex[3];
        }
        return triangles;
    }
}