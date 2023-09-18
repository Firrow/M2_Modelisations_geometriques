using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hello_Triangle : MonoBehaviour
{
    public Material mat;
    public List<Vector3> tabVector = new List<Vector3>();

    // Use this for initialization
    void Start()
    {
        //Dessiner un triangle VERSION 1
        /*gameObject.AddComponent<MeshFilter>();          // Creation d'un composant MeshFilter qui peut ensuite être visualisé
        gameObject.AddComponent<MeshRenderer>();

        Vector3[] vertices = new Vector3[3];            // Création des structures de données qui accueilleront sommets et  triangles
        int[] triangles = new int[3];


        vertices[0] = new Vector3(0, 0, 0);            // Remplissage de la structure sommet 
        vertices[1] = new Vector3(1, 0, 0);
        vertices[2] = new Vector3(0, 1, 0);


        triangles[0] = 0;                               // Remplissage de la structure triangle. Les sommets sont représentés par leurs indices
        triangles[1] = 1;                               // les triangles sont représentés par trois indices (et sont mis bout à bout)
        triangles[2] = 2;

        Mesh msh = new Mesh();                          // Création et remplissage du Mesh

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           // Remplissage du Mesh et ajout du matériel
        gameObject.GetComponent<MeshRenderer>().material = mat;*/



        //Dessiner 2 triangles (pas ensemble) VERSION 2
        /*gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Vector3 x = new Vector3(0, 0, 0);
        Vector3 y = new Vector3(1, 0, 0);
        Vector3 z = new Vector3(0, 1, 0);

        Triangle t = new Triangle(x, y, z);
        t.createTriangle(this.gameObject, t.vertices, t.triangles, mat);



        Vector3 x2 = new Vector3(0, 1, 1);
        Vector3 y2 = new Vector3(0, 0, 1);
        Vector3 z2 = new Vector3(0, 1, 0);

        Triangle t2 = new Triangle(x2, y2, z2);
        t2.createTriangle(this.gameObject, t2.vertices, t2.triangles, mat);
        //1er ne s'affiche pas quand le 2eme est décommenté*/



        //Dessiner mur triangles
        //Créer Mur de points
        PointsGrid(2, 1);

    }

    public void PointsGrid(int longueur, int largeur)
    {
        for (int i = 0; i < longueur; i++)
        {
            for (int j = 0; j < largeur; j++)
            {
                Vector3 v = new Vector3(i, j, 0);
                tabVector.Add(v); //index tab = num du point
            }
        }
    }


}



public class Triangle
{
    public Vector3 x;
    public Vector3 y;
    public Vector3 z;

    public Vector3[] vertices = new Vector3[3];
    public int[] triangles = new int[3];

    public Triangle(Vector3 s1, Vector3 s2 , Vector3 s3) //Problème avec constructeur et les vertices (n'existent pas)
    {
        this.x = s1;
        this.y = s2;
        this.z = s3;

        //Attention initialiser sommets avant vertices
        this.triangles[0] = 0;
        this.triangles[1] = 1;
        this.triangles[2] = 2;

        this.vertices[0] = x;
        this.vertices[1] = y;
        this.vertices[2] = z;
    }

    public void createTriangle(GameObject go, Vector3[] v, int[] s, Material mat)
    {
        Mesh msh = new Mesh();

        msh.vertices = v;
        msh.triangles = s;

        go.GetComponent<MeshFilter>().mesh = msh; 
        go.GetComponent<MeshRenderer>().material = mat;
    }
}