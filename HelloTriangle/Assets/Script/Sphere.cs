using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public int numberMeridian;
    public int radius;
    public int numberParallele;
    public Material material;
    public double height;

    private int pointIndex;


    void Start()
    {
        int numberPointsTotal = numberMeridian * numberParallele + 2;
        Vector3[] vertices = new Vector3[numberMeridian * numberParallele + 2];
        Vector3 Point = new Vector3();
        List<Vector3> triangles = new List<Vector3>();
        pointIndex = 0;


        //création grille (de bas en haut)
        for (int j = 1; j <= numberParallele; j++)
        {
            float phi = (float)(Math.PI * j / numberParallele);
            for (int i = 0; i < numberMeridian; i++)
            {
                float theta = 2.0f * Mathf.PI * i / numberMeridian;
                float x = (float)(radius * Math.Sin(phi) * Math.Cos(theta));
                float y = (float)(radius * Math.Sin(phi) * Math.Sin(theta));
                float z = radius * Mathf.Cos(phi);
                //faire vérification si 0 < x < numberMeridian/2 et x > numberMeridian/2 et else x == numberMeridian. Changer la formule de la création du point en fonction du cas
                //Création des points de la grille
                //Point = new Vector3(radius * Mathf.Cos((2 * Mathf.PI * i) / numberMeridian), (float)(j * space - space), radius * Mathf.Sin((2 * Mathf.PI * i) / numberMeridian));
                Point = new Vector3(x, y, z);

                //Création vertices a faire
                vertices[pointIndex] = Point;

                pointIndex++;

                //Dessin point (optionnel)
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = Point;
                sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
        }

        //Point centre face du haut et du bas cylindre
        Vector3 CentreHaut = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + radius, gameObject.transform.position.z);
        Vector3 CentreBas = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + radius);
        vertices[numberMeridian * numberParallele] = CentreHaut;
        vertices[numberMeridian * numberParallele + 1] = CentreBas;
        //Dessin point (optionnel)
        GameObject sphereHaut = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereHaut.transform.position = CentreHaut;
        sphereHaut.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        GameObject sphereBas = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereBas.transform.position = CentreBas;
        sphereBas.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);


        //Création triangles
        for (int c = 1; c < numberParallele; c++)
        {
            for (int s = 0; s < numberMeridian; s++)
            {
                if (s == numberMeridian-1)
                {
                    triangles.Add(new Vector3(numberMeridian * c - numberMeridian, numberMeridian * c + s, numberMeridian * c)); //Triangle orienté haut
                    triangles.Add(new Vector3(numberMeridian * c + s, numberMeridian * (c - 1), s + numberMeridian * (c - 1))); //Triangle orienté bas
                }
                else
                {
                    triangles.Add(new Vector3(numberMeridian * c - numberMeridian + s + 1, numberMeridian * c + s, numberMeridian * c + s + 1)); //Triangle orienté haut
                    triangles.Add(new Vector3(numberMeridian * c + s, s + (numberMeridian * c - (numberMeridian - 1)), s + numberMeridian * (c - 1))); //Triangle orienté bas
                }
            }
        }


        //création face du bas et du haut cylindre
        for (int c = 0; c < numberParallele; c++)
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
            else if (c == numberParallele-1) //face du haut
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

        Mesh msh = new Mesh();                          

        msh.vertices = vertices;

        //convertion liste de triangles en tableau de triangles
        int triangleTabSize = triangles.Count * 3;
        int[] triangleTab = new int[triangleTabSize];
        for (int i = 0; i < triangles.Count; i++)
        {
            triangleTab[i * 3] = (int)triangles[i][0];
            triangleTab[i * 3 + 1] = (int)triangles[i][1];
            triangleTab[i * 3 + 2] = (int)triangles[i][2];
        }

        msh.triangles = triangleTab;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           
        gameObject.GetComponent<MeshRenderer>().material = material;
    }

}
