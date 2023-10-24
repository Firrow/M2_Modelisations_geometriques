using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VolumesCube : MonoBehaviour
{
    public string operation;
    public List<SphereVolume> sphereList = new List<SphereVolume>();
    public int numberSphere;
    public float sizeLitteCube;

    float Xmin = float.PositiveInfinity;
    float Ymin = float.PositiveInfinity;
    float Zmin = float.PositiveInfinity;
    float Xmax = float.NegativeInfinity;
    float Ymax = float.NegativeInfinity;
    float Zmax = float.NegativeInfinity;

    private float boundingBoxSizeX;
    private float boundingBoxSizeY;
    private float boundingBoxSizeZ;

    private float numberCubeOnEdgeX;
    private float numberCubeOnEdgeY;
    private float numberCubeOnEdgeZ;

    void Start()
    {
        //Défini spheres aléatoirement
        if (numberSphere != 0)
            CreateSphere(numberSphere);
        else
        {
            //Définir spheres manuellement 
            SphereVolume s1 = new SphereVolume(4, new Vector3(8, 5, 5));
            SphereVolume s2 = new SphereVolume(5, new Vector3(10, 1, 5));
            SphereVolume s3 = new SphereVolume(7, new Vector3(3, 4, 2));
            sphereList.Add(s1);
            sphereList.Add(s2);
            sphereList.Add(s3);
        }

        //calcul taille du cube total
        CalculSizeBoundingBox();

        //Dessiner cubes
        CreateBoundingBox();
    }

    void Update()
    {

    }

    private void CreateBoundingBox()
    {
        for (int i = 0; i < numberCubeOnEdgeX; i++)
        {
            for (int j = 0; j < numberCubeOnEdgeY; j++)
            {
                for (int k = 0; k < numberCubeOnEdgeZ; k++)
                {
                    Vector3 posPetitCube = new Vector3(((boundingBoxSizeX) / numberCubeOnEdgeX) * i + Xmin, ((boundingBoxSizeY) / numberCubeOnEdgeY) * j + Ymin, ((boundingBoxSizeZ) / numberCubeOnEdgeZ) * k + Zmin);

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
        cube.transform.localScale = new Vector3(boundingBoxSizeX / numberCubeOnEdgeX, boundingBoxSizeY / numberCubeOnEdgeY, boundingBoxSizeZ / numberCubeOnEdgeZ);
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
    }

    private void CalculSizeBoundingBox()
    {
        //calculer la taille de la taille englobante en fonction sphere
        foreach (var s in this.sphereList)
        {
            Xmin = Math.Min(Xmin, s.centerSphere.x - s.radiusSphere);
            Ymin = Math.Min(Ymin, s.centerSphere.y - s.radiusSphere);
            Zmin = Math.Min(Zmin, s.centerSphere.z - s.radiusSphere);

            Xmax = Math.Max(Xmax, s.centerSphere.x + s.radiusSphere);
            Ymax = Math.Max(Ymax, s.centerSphere.y + s.radiusSphere);
            Zmax = Math.Max(Zmax, s.centerSphere.z + s.radiusSphere);
        }

        //calculer taille de chaque côté
        boundingBoxSizeX = Xmax - Xmin;
        boundingBoxSizeY = Ymax - Ymin;
        boundingBoxSizeZ = Zmax - Zmin;

        //calculer le nombre de petits cubes dans la boite grâce aux valeurs

        numberCubeOnEdgeX = boundingBoxSizeX / sizeLitteCube;
        numberCubeOnEdgeY = boundingBoxSizeY / sizeLitteCube;
        numberCubeOnEdgeZ = boundingBoxSizeZ / sizeLitteCube;

        //Debug.Log(numberCubeOnEdgeX + " " + numberCubeOnEdgeY + " " + numberCubeOnEdgeZ);
    }

    private void CreateSphere(int numberSphere)
    {
        for (int i = 0; i < numberSphere; i++)
        {
            int radius = UnityEngine.Random.Range(3, 8);
            int x = UnityEngine.Random.Range(-10, 15);
            int y = UnityEngine.Random.Range(-10, 15);
            int z = UnityEngine.Random.Range(-10, 15);

            SphereVolume s = new SphereVolume(radius, new Vector3(x, y, z));
            sphereList.Add(s);
        }
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



