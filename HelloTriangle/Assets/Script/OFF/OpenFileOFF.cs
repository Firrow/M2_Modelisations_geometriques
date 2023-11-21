using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using System.Globalization;

public class OpenFileOFF : MonoBehaviour
{
    public string file;

    private string filePath;

    void Start()
    {
        //string separator = Path.DirectorySeparatorChar.ToString();
        filePath = Application.dataPath + "/Files/" + file;
        OffReader offReader = new OffReader(filePath);

        MyMesh myMesh = new MyMesh(offReader.getVertices(), offReader.getTriangles());
        myMesh.center();
        myMesh.normalize();

        Mesh msh = new Mesh();
        msh.vertices = myMesh.getVertices();
        msh.triangles = myMesh.getTriangles();

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
            float[] vertex = lineTab[i + 2].Split(' ').Select(s => float.Parse(s, CultureInfo.InvariantCulture)).ToArray();
            vertices[i] = new Vector3(vertex[0], vertex[1], vertex[2]);
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

class MyMesh {

    private Vector3[] vertices;
    private int[] triangles;

    public MyMesh(Vector3[] vertices, int[] triangles) {
        this.vertices = vertices;
        this.triangles = triangles;
    }

    public Vector3[] getVertices() {
        return vertices;
    }

    public int[] getTriangles() {
        return triangles;
    }

    public void center() {
        Vector3 meshCenter = getGravityCenter();
        Vector3 meshCenterToSceneCenter = meshCenter - Vector3.zero;
        for (int i = 0; i < vertices.Length; i++) {
            vertices[i] -= meshCenterToSceneCenter;
        }
    }

    public void normalize() {
        float biggerDistanceToCenter = getBiggerDistanceToCenter();
        for (int i = 0; i < vertices.Length; i++) {
            vertices[i] /= biggerDistanceToCenter;
        }
    }

    private float getBiggerDistanceToCenter() {
        float biggerDistanceToCenter = 0;
        Vector3 gravityCenter = getGravityCenter();
        for (int i = 0; i < vertices.Length; i++) {
            float distanceToCenter = Vector3.Distance(vertices[i], gravityCenter);
            if (distanceToCenter > biggerDistanceToCenter) {
                biggerDistanceToCenter = distanceToCenter;
            }
        }
        return biggerDistanceToCenter;
    }

    private Vector3 getGravityCenter() {
        Vector3 gravityCenter = Vector3.zero;
        for (int i = 0; i < vertices.Length; i++) {
            gravityCenter += vertices[i];
        }
        gravityCenter /= vertices.Length;
        return gravityCenter;
    }

    //Pas obligé de le faire
    //calcul normales
    //writer
    //calculer nombre sommet, faces, arêtes
}