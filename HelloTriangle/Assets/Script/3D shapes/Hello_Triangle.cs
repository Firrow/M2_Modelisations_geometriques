using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hello_Triangle : MonoBehaviour
{
    public int width;
    public int height;
    public Material material;

    void Start()
    {
        List<Vector3> triangles = new List<Vector3>();
        Vector3[] vertices = new Vector3[width * height];

        //création grille
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if ((j < (height - 1)) && (i < (width - 1)))
                {
                    triangles.Add(new Vector3(width * j + i, width * j + i + 1, width * (j + 1) + i));
                    triangles.Add(new Vector3(width * j + i + 1, width * (j + 1) + i + 1, width * (j + 1) + i));
                }

                vertices[i + width * j] = new Vector3(i, j, 0);
            }
        }


        Mesh msh = new Mesh();

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
        gameObject.GetComponent<MeshRenderer>().material = material;
    }

       
    public void triangleProf()
    {
        //Dessiner un triangle
        gameObject.AddComponent<MeshFilter>();          // Creation d'un composant MeshFilter qui peut ensuite être visualisé
        gameObject.AddComponent<MeshRenderer>();

        Vector3[] vertices = new Vector3[6];            // Création des structures de données qui accueilleront sommets et  triangles
        int[] triangles = new int[6];


        vertices[0] = new Vector3(0, 0, 0);            // Remplissage de la structure sommet 
        vertices[1] = new Vector3(1, 0, 0);
        vertices[2] = new Vector3(0, 1, 0);
        vertices[3] = new Vector3(1, 0, 0);            // Remplissage de la structure sommet 
        vertices[4] = new Vector3(2, 0, 0);
        vertices[5] = new Vector3(1, 1, 0);


        triangles[0] = 0;                               // Remplissage de la structure triangle. Les sommets sont représentés par leurs indices
        triangles[1] = 1;                               // les triangles sont représentés par trois indices (et sont mis bout à bout)
        triangles[2] = 2;
        triangles[3] = 3;                               // Remplissage de la structure triangle. Les sommets sont représentés par leurs indices
        triangles[4] = 4;                               // les triangles sont représentés par trois indices (et sont mis bout à bout)
        triangles[5] = 5;

        Mesh msh = new Mesh();                          // Création et remplissage du Mesh

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           // Remplissage du Mesh et ajout du matériel
        gameObject.GetComponent<MeshRenderer>().material = material;
    }
}