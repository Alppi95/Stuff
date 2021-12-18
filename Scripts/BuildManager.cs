using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public NodeUI nodeUI;

    private TurretBluePrint turretToBuild;
    private Node selectedNode;

    /*turret brefabs
    public GameObject stardardTurretPrefab;
    public GameObject gatlingTurretPrefab;
    public GameObject missileTurretPrefab;
    public GameObject laserTurretPrefab;
    */

    void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }


    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }


    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.HideUI();        //hide the sell/upgrade ui when building a turret
    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
