using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermiteCurves : MonoBehaviour
{
    [SerializeField]
    public Transform[] _controlPoints;

    [SerializeField]
    public Vector3[] _vectorPoints;

    private Vector3 _gizmosPosition;

    void Update()
    {
        OnDrawGizmos();
    }

    //UTILE UNIQUEMENT POUR AFFICHER LES COURBES LORS DE LEUR PLACEMENT
    private void OnDrawGizmos()
    {
        /*formule pour courbe de Bezier cubique :
        - la formule créer une courbe entre les p1 et p4
        - p2 lié à p1 et p3 lié à p4 permettent de modeler la forme du chemin*/
        for (float t = 0; t <= 1; t += 0.01f)
        {
            //formule ne fonctionne pas ? (diapo "Cubique d'Hermite")
            _gizmosPosition = (2 * Mathf.Pow(t, 3) - 3 * Mathf.Pow(t, 2) + 1) * _controlPoints[0].position
                            + (-2 * Mathf.Pow(t, 3) + 3 * Mathf.Pow(t, 2)) * _controlPoints[1].position
                            + (Mathf.Pow(t, 3) - 2 * Mathf.Pow(t, 2) + t) * _vectorPoints[0]
                            + (Mathf.Pow(t, 3) - Mathf.Pow(t, 2)) * _vectorPoints[1];

            Gizmos.DrawSphere(_gizmosPosition, 0.25f);
        }

        //dessin de la courbe
        Gizmos.DrawLine(new Vector3(_controlPoints[0].position.x, _controlPoints[0].position.y, _controlPoints[0].position.z),
            new Vector3(_controlPoints[1].position.x, _controlPoints[1].position.y, _controlPoints[1].position.z));

    }
}
