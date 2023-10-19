using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    public int numberMeridian;
    public int radius;
    public int numberCircle;
    public Material material;
    public double height;

    private int pointIndex;
    private double space;


    void Start()
    {
        int numberPointsTotal = numberMeridian * numberCircle + 2;
        Vector3[] vertices = new Vector3[numberMeridian * numberCircle + 2];
        List<Vector3> triangles = new List<Vector3>();
        space = height / numberCircle;
        pointIndex = 0;


        //cr�ation grille (de bas en haut)
        CreateGrid(vertices);
        //Point centre face du haut et du bas cylindre
        CreatePointUpDown(vertices);


        //Cr�ation triangles
        CreateTriangles(triangles);

        //cr�ation face du bas et du haut cylindre
        CreateFacesUpDown(triangles, numberPointsTotal);

        //convertion liste de triangles en tableau de triangles
        int triangleTabSize = triangles.Count * 3;
        int[] triangleTab = new int[triangleTabSize];
        for (int i = 0; i < triangles.Count; i++)
        {
            triangleTab[i * 3] = (int)triangles[i][0];
            triangleTab[i * 3 + 1] = (int)triangles[i][1];
            triangleTab[i * 3 + 2] = (int)triangles[i][2];
        }

        Mesh msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangleTab;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           
        gameObject.GetComponent<MeshRenderer>().material = material;
    }






    private void CreateGrid(Vector3[] vertices)
    {
        Vector3 Point = new Vector3();

        for (int j = 1; j <= numberCircle; j++)
        {
            for (int i = 0; i < numberMeridian; i++)
            {
                //Cr�ation des points de la grille
                Point = new Vector3(radius * Mathf.Cos((2 * Mathf.PI * i) / numberMeridian), (float)(j * space - space), radius * Mathf.Sin((2 * Mathf.PI * i) / numberMeridian));

                //Cr�ation vertices a faire
                vertices[pointIndex] = Point;

                pointIndex++;

                //Dessin point (optionnel)
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = Point;
                sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
        }
    }

    private void CreatePointUpDown(Vector3[] vertices) {
        Vector3 CentreHaut = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (float)space * (numberCircle - 1), gameObject.transform.position.z);
        Vector3 CentreBas = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        vertices[numberMeridian * numberCircle] = CentreHaut;
        vertices[numberMeridian * numberCircle + 1] = CentreBas;
        //Dessin point (optionnel)
        GameObject sphereHaut = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereHaut.transform.position = CentreHaut;
        sphereHaut.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        GameObject sphereBas = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereBas.transform.position = CentreBas;
        sphereBas.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    private void CreateTriangles(List<Vector3> triangles)
    {
        for (int c = 1; c < numberCircle; c++)
        {
            for (int s = 0; s < numberMeridian; s++)
            {
                if (s == numberMeridian - 1)
                {
                    triangles.Add(new Vector3(numberMeridian * c - numberMeridian, numberMeridian * c + s, numberMeridian * c)); //Triangle orient� haut
                    triangles.Add(new Vector3(numberMeridian * c + s, numberMeridian * (c - 1), s + numberMeridian * (c - 1))); //Triangle orient� bas
                }
                else
                {
                    triangles.Add(new Vector3(numberMeridian * c - numberMeridian + s + 1, numberMeridian * c + s, numberMeridian * c + s + 1)); //Triangle orient� haut
                    triangles.Add(new Vector3(numberMeridian * c + s, s + (numberMeridian * c - (numberMeridian - 1)), s + numberMeridian * (c - 1))); //Triangle orient� bas
                }
            }
        }
    }

    private void CreateFacesUpDown(List<Vector3> triangles, int numberPointsTotal)
    {
        for (int c = 0; c < numberCircle; c++)
        {
            if (c == 0) //face du bas
            {
                for (int s = 0; s < numberMeridian; s++) //pour chaque point du cercle
                {
                    if (s == numberMeridian - 1) //dernier point du cercle
                    {
                        triangles.Add(new Vector3(s, 0, numberPointsTotal - 1));
                    }
                    else
                    {
                        triangles.Add(new Vector3(s, s + 1, numberPointsTotal - 1));
                    }
                }
            }
            else if (c == numberCircle - 1) //face du haut
            {
                for (int s = numberPointsTotal - numberMeridian - 2; s < numberPointsTotal - 2; s++) //pour chaque point du cercle
                {
                    if (s == numberPointsTotal - 3) //dernier point du cercle
                    {
                        triangles.Add(new Vector3(numberPointsTotal - numberMeridian - 2, s, numberPointsTotal - 2));
                    }
                    else
                    {
                        triangles.Add(new Vector3(s + 1, s, numberPointsTotal - 2));
                    }
                }
            }
        }
    }
}
