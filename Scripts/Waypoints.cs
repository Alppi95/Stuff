using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] wPoints;
    public static Transform[] wPoints2;
    public static Transform[] fPoints;
    [Tooltip("for normal enemies, leave empty if not used")]
    public GameObject wP;
    [Tooltip("for flying enemies, leave empty if not used")]
    public GameObject fP;
    [Tooltip("for reroute enemies, leave empty if not used")]
    public GameObject wP2;


    void Awake()
    {
        if (wP != null)
        {
            wPoints = new Transform[wP.transform.childCount];
            for (int i = 0; i < wPoints.Length; i++)
            {
                wPoints[i] = wP.transform.GetChild(i);
            }
        }
        if (wP2 != null)
        {
            wPoints2 = new Transform[wP2.transform.childCount];
            for (int i = 0; i < wPoints2.Length; i++)
            {
                wPoints2[i] = wP2.transform.GetChild(i);
            }
        }
        if (fP != null)
        {
            fPoints = new Transform[fP.transform.childCount];
            for (int i = 0; i < fPoints.Length; i++)
            {
                fPoints[i] = fP.transform.GetChild(i);
            }
        }
    }
}
