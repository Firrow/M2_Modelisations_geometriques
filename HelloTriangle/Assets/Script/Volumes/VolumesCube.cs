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

    private BoundingBox bd;

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
        bd = new BoundingBox(sphereList, sizeLitteCube);

        CreateCubesInsideBoundingBox();
    }

    void Update()
    {

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

    public void CreateCubesInsideBoundingBox()
    {
        for (int i = 0; i < bd.GetnumberCubeOnEdge().x; i++)
        {
            for (int j = 0; j < bd.GetnumberCubeOnEdge().y; j++)
            {
                for (int k = 0; k < bd.GetnumberCubeOnEdge().z; k++)
                {
                    Vector3 posPetitCube = new Vector3(((bd.GetSizeBoundingBox().x) / bd.GetnumberCubeOnEdge().x) * i + bd.GetXmin(), ((bd.GetSizeBoundingBox().y) / bd.GetnumberCubeOnEdge().y) * j + bd.GetYmin(), ((bd.GetSizeBoundingBox().z) / bd.GetnumberCubeOnEdge().z) * k + bd.GetZmin());

                    ChoiceUser(operation, posPetitCube);
                }
            }
        }
    }

    private void ChoiceUser(string operation, Vector3 posPetitCube)
    {
        if (operation == "intersection")
            Intersect(posPetitCube);
        else if (operation == "union")
            Union(posPetitCube);
        else
            DrawCube(posPetitCube);
    }

    private void Intersect(Vector3 posCube)
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

    private bool IsInsideIntersection(Vector3 posCube)
    {
        bool[] inside = new bool[this.sphereList.Count];
        for (int i = 0; i < this.sphereList.Count; i++)
        {
            inside[i] = Math.Pow(posCube.x - sphereList[i].centerSphere.x, 2) + Math.Pow(posCube.y - sphereList[i].centerSphere.y, 2) + Math.Pow(posCube.z - sphereList[i].centerSphere.z, 2) - Math.Pow(sphereList[i].radiusSphere, 2) < 0;
        }

        return inside.All(val => val == true);
    }

    private bool IsInsideUnion(Vector3 posCube)
    {
        bool[] inside = new bool[this.sphereList.Count];
        for (int i = 0; i < this.sphereList.Count; i++)
        {
            inside[i] = Math.Pow(posCube.x - sphereList[i].centerSphere.x, 2) + Math.Pow(posCube.y - sphereList[i].centerSphere.y, 2) + Math.Pow(posCube.z - sphereList[i].centerSphere.z, 2) - Math.Pow(sphereList[i].radiusSphere, 2) < 0;
        }

        return inside.Any(val => val == true);
    }

    private void DrawCube(Vector3 posCube)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = posCube;
        cube.transform.localScale = new Vector3(bd.GetSizeBoundingBox().x / bd.GetnumberCubeOnEdge().x, bd.GetSizeBoundingBox().y / bd.GetnumberCubeOnEdge().y, bd.GetSizeBoundingBox().z / bd.GetnumberCubeOnEdge().z);
    }

}




public class BoundingBox
{
    private Vector3 sizeBoundingBox = new Vector3();
    private Vector3 numberCubeOnEdge = new Vector3();

    private float Xmin = float.PositiveInfinity;
    private float Ymin = float.PositiveInfinity;
    private float Zmin = float.PositiveInfinity;
    private float Xmax = float.NegativeInfinity;
    private float Ymax = float.NegativeInfinity;
    private float Zmax = float.NegativeInfinity;


    public BoundingBox(List<SphereVolume> listSphere, float sizeLitteCube)
    {
        this.sizeBoundingBox = CalculSizeBoundingBox(listSphere);
        this.numberCubeOnEdge = CalculNumberCube(sizeBoundingBox, sizeLitteCube);
    }

    public Vector3 CalculSizeBoundingBox(List<SphereVolume> listSphere)
    {
        //calculer la taille de la taille englobante en fonction sphere
        foreach (var s in listSphere)
        {
            Xmin = Math.Min(Xmin, s.centerSphere.x - s.radiusSphere);
            Ymin = Math.Min(Ymin, s.centerSphere.y - s.radiusSphere);
            Zmin = Math.Min(Zmin, s.centerSphere.z - s.radiusSphere);

            Xmax = Math.Max(Xmax, s.centerSphere.x + s.radiusSphere);
            Ymax = Math.Max(Ymax, s.centerSphere.y + s.radiusSphere);
            Zmax = Math.Max(Zmax, s.centerSphere.z + s.radiusSphere);
        }

        //calculer taille de chaque côté
        float boundingBoxSizeX = Xmax - Xmin;
        float boundingBoxSizeY = Ymax - Ymin;
        float boundingBoxSizeZ = Zmax - Zmin;

        return new Vector3(boundingBoxSizeX, boundingBoxSizeY, boundingBoxSizeZ);
    }

    public Vector3 CalculNumberCube(Vector3 boundingBoxSize, float sizeLitteCube)
    {
        //calculer le nombre de petits cubes dans la boite grâce aux valeurs
        float numberCubeOnEdgeX = boundingBoxSize.x / sizeLitteCube;
        float numberCubeOnEdgeY = boundingBoxSize.y / sizeLitteCube;
        float numberCubeOnEdgeZ = boundingBoxSize.z / sizeLitteCube;

        return new Vector3(numberCubeOnEdgeX, numberCubeOnEdgeY, numberCubeOnEdgeZ);
    }

    // Do last question


    public Vector3 GetSizeBoundingBox()
    {
        return this.sizeBoundingBox;
    }

    public Vector3 GetnumberCubeOnEdge()
    {
        return this.numberCubeOnEdge;
    }
    public float GetXmin()
    {
        return this.Xmin;
    }
    public float GetXmax()
    {
        return this.Xmax;
    }
    public float GetYmin()
    {
        return this.Ymin;
    }
    public float GetYmax()
    {
        return this.Ymax;
    }
    public float GetZmin()
    {
        return this.Zmin;
    }
    public float GetZmax()
    {
        return this.Zmax;
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



