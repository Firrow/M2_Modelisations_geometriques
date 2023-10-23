using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumesCube : MonoBehaviour
{
    public float cubeSize;
    public float numberCubeOnEdge;

    public float radiusSphere1;
    public float radiusSphere2;
    public Vector3 centerSphere1;
    public Vector3 centerSphere2;

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
                    Vector3 posCube = new Vector3(((cubeSize) / numberCubeOnEdge) * i - cubeSize / 2, ((cubeSize) / numberCubeOnEdge) * j - cubeSize / 2, ((cubeSize) / numberCubeOnEdge) * k - cubeSize / 2);

                    //on dessine cube si le centre du cube est dans la sphère
                    DrawSphere(posCube, centerSphere1, radiusSphere1);
                    DrawSphere(posCube, centerSphere2, radiusSphere2);
                }
            }
        }
    }

    private void DrawSphere(Vector3 posCube, Vector3 CenterSphere, float radiusSphere)
    {
        if (Math.Pow(posCube.x-CenterSphere.x, 2) + Math.Pow(posCube.y - CenterSphere.y, 2) + Math.Pow(posCube.z - CenterSphere.z, 2) - Math.Pow(radiusSphere, 2) < 0)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = posCube;
            cube.transform.localScale = new Vector3(cubeSize / numberCubeOnEdge, cubeSize / numberCubeOnEdge, cubeSize / numberCubeOnEdge);
        }
    }
}

