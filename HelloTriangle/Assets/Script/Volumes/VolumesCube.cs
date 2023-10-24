using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumesCube : MonoBehaviour
{
    public string operation;
    public float radiusSphere1;
    public float radiusSphere2;
    public Vector3 centerSphere1;
    public Vector3 centerSphere2;

    private float boundingBoxSize;
    private float numberCubeOnEdge;

    void Start()
    {
        //Dessiner cubes
        CreateBoundingBox();
    }

    void Update()
    {

    }

    private void CreateBoundingBox()
    {
        for (int i = 0; i < numberCubeOnEdge; i++)
        {
            for (int j = 0; j < numberCubeOnEdge; j++)
            {
                for (int k = 0; k < numberCubeOnEdge; k++)
                {
                    Vector3 posCube = new Vector3(((boundingBoxSize) / numberCubeOnEdge) * i - boundingBoxSize / 2, ((boundingBoxSize) / numberCubeOnEdge) * j - boundingBoxSize / 2, ((boundingBoxSize) / numberCubeOnEdge) * k - boundingBoxSize / 2);

                    ChoiceUser(operation, posCube, centerSphere1, centerSphere2, radiusSphere1, radiusSphere2);
                }
            }
        }
    }

    private void Intersect(Vector3 posCube, Vector3 CenterSphere1, Vector3 CenterSphere2, float radiusSphere1, float radiusSphere2)
    {
        if (IsInside(posCube, centerSphere1, radiusSphere1) && IsInside(posCube, centerSphere2, radiusSphere2))
        {
            DrawCube(posCube);
        }
    }

    private void Union(Vector3 posCube, Vector3 CenterSphere1, Vector3 CenterSphere2, float radiusSphere1, float radiusSphere2)
    {
        if (IsInside(posCube, centerSphere1, radiusSphere1) || IsInside(posCube, centerSphere2, radiusSphere2))
        {
            DrawCube(posCube);
        }
    }

    private void DrawCube(Vector3 posCube)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = posCube;
        cube.transform.localScale = new Vector3(boundingBoxSize / numberCubeOnEdge, boundingBoxSize / numberCubeOnEdge, boundingBoxSize / numberCubeOnEdge);
    }




    private bool IsInside(Vector3 posCube, Vector3 CenterSphere, float radiusSphere)
    {
        return Math.Pow(posCube.x - CenterSphere.x, 2) + Math.Pow(posCube.y - CenterSphere.y, 2) + Math.Pow(posCube.z - CenterSphere.z, 2) - Math.Pow(radiusSphere, 2) < 0;
    }

    private void ChoiceUser(string operation, Vector3 posCube, Vector3 CenterSphere1, Vector3 CenterSphere2, float radiusSphere1, float radiusSphere2)
    {
        if (operation == "intersection")
            Intersect(posCube, CenterSphere1, CenterSphere2, radiusSphere1, radiusSphere2);
        else if (operation == "union")
            Union(posCube, CenterSphere1, CenterSphere2, radiusSphere1, radiusSphere2);
        else
            throw new Exception("Opération inconnue du système.");
    }

    /*private void CalculSizeBoundingBox(Vector3 CenterSphere1, Vector3 CenterSphere2, float radiusSphere1, float radiusSphere2)
    {
        //calculer la taille de la taille englobante en fonction sphere
        //calculer la taille des petits cubes dans la boite
    }*/
} 

public class Sphere
{
    public float radiusSphere;
    public Vector3 centerSphere;

    public Sphere(float radius, Vector3 position)
    {
        this.radiusSphere = radius;
        this.centerSphere = position;
    }
}

