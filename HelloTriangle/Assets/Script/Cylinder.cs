using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    public int numberMeridian;
    public int radius;
    public int height;
    public Material material;

    void Start()
    {
        int numberPointsSides = numberMeridian * height;
        int numberPointsTotal = numberMeridian * height + 2;

        Vector3[] vertices = new Vector3[numberMeridian * height + 2];
        Vector3 Pi = new Vector3();
        List<Vector3> triangles = new List<Vector3>();


        //création grille
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < numberMeridian; i++)
            {
                //Création des points de la grille
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
        }

        //Point centre face du haut et du bas cylindre
        Vector3 CentreHaut = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + height / 2, gameObject.transform.position.z);
        Vector3 CentreBas = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - height / 2, gameObject.transform.position.z);
        vertices[numberMeridian * height] = CentreHaut;
        vertices[numberMeridian * height + 1] = CentreBas;
        GameObject sphereHaut = GameObject.CreatePrimitive(PrimitiveType.Sphere); //partie temporaire
        sphereHaut.transform.position = CentreHaut;
        sphereHaut.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        GameObject sphereBas = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereBas.transform.position = CentreBas;
        sphereBas.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);


        //Création triangles
        for (int s = 0; s < numberMeridian; s++)
        {
            if (s == numberMeridian-1)
            {
                triangles.Add(new Vector3(s, 0, numberPointsSides - 1)); //Triangle orienté haut
                triangles.Add(new Vector3(0, numberPointsSides/2, numberPointsSides - 1)); //Triangle orienté bas
            }
            else
            {
                triangles.Add(new Vector3(s, s + 1, numberMeridian + s)); //Triangle orienté haut
                triangles.Add(new Vector3(s + 1, numberPointsSides/2 + s + 1, numberPointsSides/2 + s)); //Triangle orienté bas
            }
        }

        //création sommet cylindre
        for (int s = 0; s < numberPointsTotal; s++)
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
        }

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
