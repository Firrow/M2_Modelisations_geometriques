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


        //cr�ation grille
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < numberMeridian; i++)
            {
                //Cr�ation des points de la grille
                if (j == 1)
                {
                    Debug.Log("Bas !");
                    Pi = new Vector3(radius * Mathf.Cos((2 * Mathf.PI * i) / numberMeridian), -height / 2, radius * Mathf.Sin((2 * Mathf.PI * i) / numberMeridian));
                }
                else
                {
                    Pi = new Vector3(radius * Mathf.Cos((2 * Mathf.PI * i) / numberMeridian), height / 2, radius * Mathf.Sin((2 * Mathf.PI * i) / numberMeridian));
                }

                //Cr�ation vertices a faire
                vertices[i + numberMeridian * j] = Pi;

                //Dessin point
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = Pi;
                sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
        }

   
        //Cr�ation triangles
        for (int s = 0; s < numberMeridian; s++)
        {
            //Cr�ation des triangles
            if (s == numberMeridian-1)
            {
                triangles.Add(new Vector3(s, 0, numberPoints - 1)); //Triangle orient� haut
                triangles.Add(new Vector3(0, numberPoints/2, numberPoints - 1)); //Triangle orient� bas
            }
            else
            {
                triangles.Add(new Vector3(s, s + 1, numberMeridian + s)); //Triangle orient� haut
                triangles.Add(new Vector3(s + 1, numberPoints/2 + s + 1, numberPoints/2 + s)); //Triangle orient� bas
            }
        }

        //TODO
        //Dessiner triangles en haut et en bas du cylindre (attention sens des normales)


        Mesh msh = new Mesh();                          


        gameObject.AddComponent<MeshFilter>();          
        gameObject.AddComponent<MeshRenderer>();

        msh.vertices = vertices;

        //Permet de passer d'une liste � un tableau de int
        int triangleTabSize = triangles.Count * 3;
        int[] triangleTab = new int[triangleTabSize];
        for (int i = 0; i < triangles.Count; i++)
        {
            triangleTab[i * 3] = (int)triangles[i][0];
            triangleTab[i * 3 + 1] = (int)triangles[i][1];
            triangleTab[i * 3 + 2] = (int)triangles[i][2];
        }

        msh.triangles = triangleTab;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           // Remplissage du Mesh et ajout du mat�riel
        gameObject.GetComponent<MeshRenderer>().material = material;
    }

    void Update()
    {
        
    }



    public void CreateWallTriangles(int numberMeridian, int height, int radius, Material material)
    {

    }

}
