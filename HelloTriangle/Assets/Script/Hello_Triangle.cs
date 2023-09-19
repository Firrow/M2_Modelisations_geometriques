using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hello_Triangle : MonoBehaviour
{
    public Material material;
    private List<Vector3> tabVector = new List<Vector3>();
    public int widthGrid;
    public int heightGrid;
    private int numberTriangles;

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

        //PEUT ÊTRE UTILE
        //longueur * j + i : permet d'avoir indice élément de chaque ligne




        //Dessiner mur triangles---------------------------------------------

        //Créer Mur de points
        widthGrid = 5;
        heightGrid = 2;
        tabVector = PointsGrid(widthGrid, heightGrid);

        //Créer un triangle à partir de 3 points de la grille
        numberTriangles = (widthGrid - 1) * (heightGrid - 1) * 2;

        for (int i = 1; i <= numberTriangles; i++)
        {
            int lineNumber = 0;
            /*if(numberTriangles%2 == 0) //triangle du haut
            {

            }
            else //triangle du bas
            {

            }
           */

            for (int point = 1; point < tabVector.Count; point++)
            {
                //mettre vérification triangles ici
                if (point + 1 <= widthGrid) //tant que l'on a pas atteint le bout de la grille
                {
                    Debug.Log(point);
                    Triangle t = new Triangle(tabVector[point], tabVector[point + 1], tabVector[widthGrid * lineNumber + (point % widthGrid)], true);
                    t.createTriangle(this.gameObject, t.vertices, t.triangles, material);
                }
                
            }
            
        }

        //Dessiner les triangles
        //createTriangle();

    }

    public List<Vector3> PointsGrid(int width, int height)
    {
        List<Vector3> tab = new List<Vector3>();

        for (int i = 1; i <= height; i++)
        {
            for (int j = 1; j <= width; j++)
            {
                Vector3 v = new Vector3(i, j, 0); //création point de la grille
                tab.Add(v); //index tab = num du point
                //Debug.Log("Point " + tabVector.IndexOf(v) + " : x = " + i + "     y = " + j); 
            }
        }
        return tab;
    }


}



public class Triangle
{
    public Vector3 x;
    public Vector3 y;
    public Vector3 z;

    public Vector3[] vertices = new Vector3[3];
    public int[] triangles = new int[3];

    public Triangle(Vector3 s1, Vector3 s2 , Vector3 s3, bool triangleDirection) //Problème avec constructeur et les vertices (n'existent pas)
    {
        this.x = s1;
        this.y = s2;
        this.z = s3;

        //Attention initialiser sommets avant vertices
        //Permet de dessiner les 2 types de triangles (voir carnet pour correspondance sommet/index)
        if(triangleDirection == true)
        {
            this.triangles[0] = 0;
            this.triangles[1] = 1;
            this.triangles[2] = 2;
        }
        else
        {
            this.triangles[0] = 2;
            this.triangles[1] = 1;
            this.triangles[2] = 0;
        }

        this.vertices[0] = x;
        this.vertices[1] = y;
        this.vertices[2] = z;
    }

    public void createTriangle(GameObject go, Vector3[] v, int[] s, Material mat)
    {
        Mesh msh = new Mesh();

        //tout ça doit s'exécuter pour tous les triangles d'un coup (un seul draw)
        msh.vertices = v;
        msh.triangles = s;

        go.GetComponent<MeshFilter>().mesh = msh; 
        go.GetComponent<MeshRenderer>().material = mat;
    }
}