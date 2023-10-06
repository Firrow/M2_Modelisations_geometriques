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

    /*public int numberMeridian;
    public int radius;
    public int height;
    public int numberCircle;
    public Material material;*/

    private int pointIndex = 0;
    private double space;

    void Start()
    {
        int numberPointsSides = numberMeridian * numberCircle;
        int numberPointsTotal = numberMeridian * 2 + 2;

        Vector3[] vertices = new Vector3[numberMeridian * numberCircle + 2];
        Vector3 Pi = new Vector3();
        List<Vector3> triangles = new List<Vector3>();
        space = height / numberCircle;


        //création grille (de bas en haut)
        for (int j = 1; j <= numberCircle; j++)
        {
            for (int i = 0; i < numberMeridian; i++)
            {
                //Création des points de la grille
                //TODO : ne pas tester j == 1 et autre --> inclure j dans les formules et faire en sorte que 1ère boucle parcourt tous les cercles
                
                Pi = new Vector3(radius * Mathf.Cos((2 * Mathf.PI * i) / numberMeridian), (float)(j * space - space), radius * Mathf.Sin((2 * Mathf.PI * i) / numberMeridian));

                //Création vertices a faire
                vertices[pointIndex] = Pi;

                //Debug.Log("nombre : " + pointIndex);
                pointIndex++;

                //Dessin point
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = Pi;
                sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
        }

        //création grille BASE
        /*for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < numberMeridian; i++)
            {
                //Création des points de la grille
                //TODO : ne pas tester j == 1 et autre --> inclure j dans les formules et faire en sorte que 1ère boucle parcourt tous les cercles
                if (j == 1)
                {
                    Pi = new Vector3(radius * Mathf.Cos((2 * Mathf.PI * i) / numberMeridian), -height / 2, radius * Mathf.Sin((2 * Mathf.PI * i) / numberMeridian));
                }
                else
                {
                    Pi = new Vector3(radius * Mathf.Cos((2 * Mathf.PI * i) / numberMeridian), height / 2, radius * Mathf.Sin((2 * Mathf.PI * i) / numberMeridian));
                }

                //Création vertices a faire
                vertices[i + numberMeridian * j] = Pi;

                //Dessin point
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = Pi;
                sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
        }*/

        Debug.Log("Space : " + space);

        //Point centre face du haut et du bas cylindre
        Vector3 CentreHaut = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (float)space * (numberCircle-1), gameObject.transform.position.z);
        Vector3 CentreBas = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        vertices[numberMeridian * numberCircle] = CentreHaut;
        vertices[numberMeridian * numberCircle + 1] = CentreBas;
        GameObject sphereHaut = GameObject.CreatePrimitive(PrimitiveType.Sphere); //partie temporaire
        sphereHaut.transform.position = CentreHaut;
        sphereHaut.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        GameObject sphereBas = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereBas.transform.position = CentreBas;
        sphereBas.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        //Point centre face du haut et du bas cylindre BASE
        /*Vector3 CentreHaut = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (float)height / 2, gameObject.transform.position.z);
        Vector3 CentreBas = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - (float)height / 2, gameObject.transform.position.z);
        vertices[numberMeridian * height] = CentreHaut;
        vertices[numberMeridian * height + 1] = CentreBas;
        GameObject sphereHaut = GameObject.CreatePrimitive(PrimitiveType.Sphere); //partie temporaire
        sphereHaut.transform.position = CentreHaut;
        sphereHaut.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        GameObject sphereBas = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereBas.transform.position = CentreBas;
        sphereBas.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);*/


        //Création triangles
        for (int c = 1; c < numberCircle; c++)
        {
            Debug.Log("--------------------------------------------");
            for (int s = 0; s < numberMeridian; s++)
            {
                int temp1 = s + numberMeridian * (c - 1);
                int temp2 = s + (numberMeridian * c - (numberMeridian - 1));
                int temp3 = numberMeridian * c + s;
                int temp5 = numberMeridian * c + s + 1;
                int temp6 = numberMeridian * c - numberMeridian + s + 1;
                int temp7 = numberMeridian * (c - 1);
                int temp8 = numberMeridian * c + s;
                int temp9 = numberMeridian * c;
                int temp10 = numberMeridian * c - numberMeridian;
                int temp11 = numberMeridian * c;


                Debug.Log("cercle : " + c + " || sommet : " + s);
                

                if (s == numberMeridian-1)
                {
                    Debug.Log("haut FINAL: " + temp10 + " " + temp8 + " " + temp11);
                    Debug.Log("bas FINAL : " + temp3 + " " + temp7 + " " + temp1);
                    triangles.Add(new Vector3(numberMeridian * c - numberMeridian, numberMeridian * c + s, numberMeridian * c)); //Triangle orienté haut
                    triangles.Add(new Vector3(numberMeridian * c + s, numberMeridian * (c - 1), s + numberMeridian * (c - 1))); //Triangle orienté bas
                }
                else
                {
                    Debug.Log("haut : " + temp6 + " " + temp3 + " " + temp5);
                    Debug.Log("bas : " + temp3 + " " + temp2 + " " + temp1);
                    triangles.Add(new Vector3(numberMeridian * c - numberMeridian + s + 1, numberMeridian * c + s, numberMeridian * c + s + 1)); //Triangle orienté haut
                    triangles.Add(new Vector3(numberMeridian * c + s, s + (numberMeridian * c - (numberMeridian - 1)), s + numberMeridian * (c - 1))); //Triangle orienté bas
                }
            }
        }



        /* //Création triangles
        for (int s = 0; s < numberMeridian; s++)
        {
            if (s == numberMeridian - 1)
            {
                triangles.Add(new Vector3(s, 0, numberPointsSides - 1)); //Triangle orienté haut
                //triangles.Add(new Vector3(0, numberPointsSides/2, numberPointsSides - 1)); //Triangle orienté bas
            }
            else
            {
                triangles.Add(new Vector3(s, s + 1, numberMeridian + s)); //Triangle orienté haut
                //triangles.Add(new Vector3(s + 1, numberPointsSides/2 + s + 1, numberPointsSides/2 + s)); //Triangle orienté bas
            }
        }*/

        //création face du haut et du bas cylindre
        /*for (int s = 0; s < numberPointsTotal; s++)
        {
            if (s <= numberPointsTotal / 2 - 2)
            {
                if (s == numberPointsTotal / 2 - 2)
                {
                    triangles.Add(new Vector3(numberPointsTotal - 1, 0, s));
                }
                else
                {
                    triangles.Add(new Vector3(numberPointsTotal - 1, s + 1, s));
                }
            }
            else if (s >= numberPointsTotal / 2 - 1 && s < numberPointsTotal - 2)
            {
                if (s == numberPointsTotal - 3)
                {
                    triangles.Add(new Vector3(s, numberPointsTotal / 2 - 1, numberPointsTotal - 1));
                }
                else
                {
                    triangles.Add(new Vector3(s, s + 1, numberPointsTotal - 1));
                }
            }
        }*/

        Mesh msh = new Mesh();                          

        msh.vertices = vertices;


        int triangleTabSize = triangles.Count * 3;
        int[] triangleTab = new int[triangleTabSize];
        for (int i = 0; i < triangles.Count; i++)
        {
            triangleTab[i * 3] = (int)triangles[i][0];
            triangleTab[i * 3 + 1] = (int)triangles[i][1];
            triangleTab[i * 3 + 2] = (int)triangles[i][2];
        }

        msh.triangles = triangleTab;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           // Remplissage du Mesh et ajout du matériel
        gameObject.GetComponent<MeshRenderer>().material = material;
    }

}
