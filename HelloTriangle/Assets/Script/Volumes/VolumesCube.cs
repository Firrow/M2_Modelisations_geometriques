using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VolumesCube : MonoBehaviour
{
    public string operation;
    public List<SphereVolume> sphereList = new List<SphereVolume>();

    public float boundingBoxSize;
    public float numberCubeOnEdge;

    void Start()
    {
        //Dessiner cubes
        SphereVolume s1 = new SphereVolume(3, new Vector3(6, 5, 5));
        SphereVolume s2 = new SphereVolume(3, new Vector3(2, 2, 2));
        SphereVolume s3 = new SphereVolume(3, new Vector3(0, 0, 3));
        sphereList.Add(s1);
        sphereList.Add(s2);
        sphereList.Add(s3);

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
                    Vector3 posPetitCube = new Vector3(((boundingBoxSize) / numberCubeOnEdge) * i - boundingBoxSize / 2, ((boundingBoxSize) / numberCubeOnEdge) * j - boundingBoxSize / 2, ((boundingBoxSize) / numberCubeOnEdge) * k - boundingBoxSize / 2);

                    ChoiceUser(operation, posPetitCube);
                }
            }
        }
    }

    private void Intersect(Vector3 posCube) //Vector3 posCube, Vector3 CenterSphere1, Vector3 CenterSphere2, float radiusSphere1, float radiusSphere2
    {
        if (IsInsideIntersection(posCube))
        {
            DrawCube(posCube);
        }
    }

    private void Union(Vector3 posPetitCube)
    {
        if (IsInsideUnion(posPetitCube))
        {
            DrawCube(posPetitCube);
        }
    }

    private void DrawCube(Vector3 posPetitCube)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = posPetitCube;
        cube.transform.localScale = new Vector3(boundingBoxSize / numberCubeOnEdge, boundingBoxSize / numberCubeOnEdge, boundingBoxSize / numberCubeOnEdge);
    }




    private bool IsInsideIntersection(Vector3 posCube) //Vector3 posCube, Vector3 CenterSphere, float radiusSphere
    {
        bool[] inside = new bool[this.sphereList.Count];
        for (int i = 0; i < this.sphereList.Count; i++)
        {
            inside[i] = Math.Pow(posCube.x - sphereList[i].centerSphere.x, 2) + Math.Pow(posCube.y - sphereList[i].centerSphere.y, 2) + Math.Pow(posCube.z - sphereList[i].centerSphere.z, 2) - Math.Pow(sphereList[i].radiusSphere, 2) < 0;
        }

        return inside.All(val => val == true);
    }

    private bool IsInsideUnion(Vector3 posCube) //Vector3 posCube, Vector3 CenterSphere, float radiusSphere
    {
        bool[] inside = new bool[this.sphereList.Count];
        for (int i = 0; i < this.sphereList.Count; i++)
        {
            inside[i] = Math.Pow(posCube.x - sphereList[i].centerSphere.x, 2) + Math.Pow(posCube.y - sphereList[i].centerSphere.y, 2) + Math.Pow(posCube.z - sphereList[i].centerSphere.z, 2) - Math.Pow(sphereList[i].radiusSphere, 2) < 0;
        }

        return inside.Any(val => val == true);
    }

    private void ChoiceUser(string operation, Vector3 posPetitCube) //string operation, Vector3 posCube, Vector3 CenterSphere1, Vector3 CenterSphere2, float radiusSphere1, float radiusSphere2
    {
        if (operation == "intersection")
            Intersect(posPetitCube);
        else if (operation == "union")
            Union(posPetitCube);
        else
            DrawCube(posPetitCube);
            //throw new Exception("Opération inconnue du système.");
    }

    private void CalculSizeBoundingBox()
    {
        //calculer la taille de la taille englobante en fonction sphere

        //calculer la taille des petits cubes dans la boite
    }
}

public class SphereVolume
{
    public float radiusSphere;
    public Vector3 centerSphere;

    public SphereVolume(float radius, Vector3 position)
    {
        this.radiusSphere = radius;
        this.centerSphere = position;
    }
}



