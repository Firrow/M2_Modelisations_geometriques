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
        int numberPoints = numberMeridian * 2;

        Vector3[] vertices = new Vector3[numberMeridian * height];
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
                    Debug.Log("Bas !");
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

        //vertices[i + numberMeridian * j] = Pi;

        Debug.Log("longueur tableau : " + vertices.Length);

        //Création triangles
        //GOOD HAUT
        /*triangles.Add(new Vector3(0, 1, 4));
        triangles.Add(new Vector3(1, 2, 5));
        triangles.Add(new Vector3(2, 3, 6));
        triangles.Add(new Vector3(3, 0, 7));*/

        /*triangles.Add(new Vector3(1, 5, 4));
        triangles.Add(new Vector3(2, 6, 5));
        triangles.Add(new Vector3(3, 7, 6));
        triangles.Add(new Vector3(0, 4, 7));*/


        for (int s = 0; s < numberMeridian; s++)
        {
            //Création des triangles
            if (s == numberMeridian-1)
            {
                triangles.Add(new Vector3(s, 0, numberPoints - 1)); //haut
                triangles.Add(new Vector3(0, numberPoints/2, numberPoints - 1)); //bas
            }
            else
            {
                triangles.Add(new Vector3(s, s + 1, numberMeridian + s)); //haut
                triangles.Add(new Vector3(s + 1, numberPoints/2 + s + 1, numberPoints/2 + s)); //bas
            }
            //Debug.Log(triangles[s]);
        }


        //A FAIRE : 
        //trouver formule dynamique ordre des points des triangles

        Mesh msh = new Mesh();                          


        gameObject.AddComponent<MeshFilter>();          
        gameObject.AddComponent<MeshRenderer>();

        msh.vertices = vertices;

        //Permet de passer d'une liste à un tableau de int
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

    void Update()
    {
        
    }



    public void CreateWallTriangles(int numberMeridian, int height, int radius, Material material)
    {

    }

}
