using System.Collections.Generic;
using UnityEngine;

public class Way_Point_Holder : MonoBehaviour
{

    public Color _WayPoinColor;
    public float _SpereSize = 1.0f;

    List<Transform> nodes = new List<Transform>();

    private void OnDrawGizmos()
    {
        Gizmos.color = _WayPoinColor;
        Transform[] pathTransforms = GetComponentsInChildren<Transform>();

        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentNode = nodes[i].position;
            Vector3 previousNode = Vector3.zero;

            if (i > 0)
            {
                previousNode = nodes[i - 1].position;

                if(nodes[i].GetComponent<Lap_CheckPont>())
                {
                    Lap_CheckPont updateIndext = nodes[i].GetComponent<Lap_CheckPont>();

                    updateIndext._Index = i + 1;
                }
                else if(nodes[i].GetComponent<Lap_Counter>())
                {
                    Lap_Counter lapUpdater = nodes[i].GetComponent<Lap_Counter>();
                    lapUpdater._TotalCheckPoints = nodes.Count - 1;
                }
            }
            else if (i == 0 && nodes.Count > 1)
            {
                previousNode = nodes[nodes.Count - 1].position;
            }

            Gizmos.DrawLine(previousNode, currentNode);
            Gizmos.DrawSphere(currentNode, _SpereSize);

        }
    }

}
