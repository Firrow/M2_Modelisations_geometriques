using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermiteBeziercurves : MonoBehaviour
{
    [SerializeField]
    public Transform[] _controlPoints;

    private Vector3 _gizmosPosition;

    void Update()
    {
        OnDrawGizmos();
    }

    //UTILE UNIQUEMENT POUR AFFICHER LES COURBES LORS DE LEUR PLACEMENT
    private void OnDrawGizmos()
    {
        /*formule pour courbe de Bezier cubique :
        - la formule cr�er une courbe entre les p1 et p4
        - p2 li� � p1 et p3 li� � p4 permettent de modeler la forme du chemin*/
        for (float t = 0; t <= 1; t += 0.05f)
        {
            _gizmosPosition = Mathf.Pow(1 - t, 3) * _controlPoints[0].position +
                              3 * Mathf.Pow(1 - t, 2) * t * _controlPoints[1].position +
                              3 * (1 - t) * Mathf.Pow(t, 2) * _controlPoints[2].position +
                              Mathf.Pow(t, 3) * _controlPoints[3].position;

            Gizmos.DrawSphere(_gizmosPosition, 0.25f);
        }

        //dessin de la courbe
        Gizmos.DrawLine(new Vector3(_controlPoints[0].position.x, _controlPoints[0].position.y, _controlPoints[0].position.z),
            new Vector3(_controlPoints[1].position.x, _controlPoints[1].position.y, _controlPoints[1].position.z));
        Gizmos.DrawLine(new Vector3(_controlPoints[2].position.x, _controlPoints[2].position.y, _controlPoints[2].position.z),
            new Vector3(_controlPoints[3].position.x, _controlPoints[3].position.y, _controlPoints[3].position.z));

    }
}
