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
        CreateWallTriangles(numberMeridian, height, radius, material);
    }

    void Update()
    {
        
    }



    public void CreateWallTriangles(int meridian, int height, int radius, Material material)
    {
        List<Vector3> triangles = new List<Vector3>();
        Vector3[] vertices = new Vector3[meridian * height];
        Vector3 Pi = new Vector3();

        //création grille
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < meridian; i++)
            {
                //Création des points de la grille
                if (j == 1)
                {
                    Debug.Log("Bas !");
                    Pi = new Vector3(radius * Mathf.Cos((2 * Mathf.PI * i) / meridian), -height / 2, radius * Mathf.Sin((2 * Mathf.PI * i) / meridian));
                }
                else
                {
                    Pi = new Vector3(radius * Mathf.Cos((2 * Mathf.PI * i) / meridian), height / 2, radius * Mathf.Sin((2 * Mathf.PI * i) / meridian));
                }
               
                //vertices[i + meridian * j] = Pi;

                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = Pi;
                sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);


                //Création vertices a faire
                vertices[i + meridian * j] = Pi;

                //Création triangles


                /*if ((j < (height - 1)) && (i < (meridian - 1)))
                {
                    //triangles.Add(new Vector3(numberPoints * j + i, numberPoints * j + i + 1, numberPoints * (j + 1) + i));
                    //triangles.Add(new Vector3(numberPoints * j + i + 1, numberPoints * (j + 1) + i + 1, numberPoints * (j + 1) + i));

                    triangles.Add(new Vector3(meridian * j + i, meridian * j + i + 1, meridian * (j + 1) + i));
                    triangles.Add(new Vector3(meridian * j + i + 1, meridian * (j + 1) + i + 1, meridian * (j + 1) + i));
                }
                else
                {
                    //coller points 1ère et dernière ligne
                }*/
            }
        }

        for(int v=0; v < vertices.Length; v++)
        {
            Debug.Log("index : " + v + " | valeur : " + vertices[v]);
        }

        /*Mesh msh = new Mesh();

        gameObject.AddComponent<MeshFilter>();          // Creation d'un composant MeshFilter qui peut ensuite être visualisé
        gameObject.AddComponent<MeshRenderer>();

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
        gameObject.GetComponent<MeshRenderer>().material = material;*/
    }
}
