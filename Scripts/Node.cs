using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    BuildManager buildManager;

    public Color hoverColor;
    public Color notEnoughMoneyColour;
    public Vector3 positionOffSet;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBlue;
    [HideInInspector]
    public bool isUpgraded = false;


    private Renderer rend;
    private Color startColor;

    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffSet;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }


        if(turret != null)
        {
            buildManager.SelectNode(this); //pass the current node to buildmanager
            return;
        }
        if (!buildManager.CanBuild)
        {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBluePrint turretBluePrint)
    {
        if (PlayerStats.Money < turretBluePrint.cost)
        {
            //cool sound maybe??
            return;
        }
        PlayerStats.Money -= turretBluePrint.cost;

        GameObject _turret = (GameObject)Instantiate(turretBluePrint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turretBlue = turretBluePrint;
        turret.GetComponent<Turret>().node = this;
        Debug.Log("turret built.");
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlue.upgradeCost)
        {
            //cool sound maybe??
            Debug.Log("no money bruh");
            return;
        }
        PlayerStats.Money -= turretBlue.upgradeCost;

        //removes the non upgraded one before new one is instantiated
        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBlue.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turret.GetComponent<Turret>().node = this;
        isUpgraded = true;
        Debug.Log("turret upped.");
    }
    
    public void SellTurret()
    {
        PlayerStats.Money += turretBlue.GetSellAmount();    //gives money based on the sell value
        Destroy(turret);    //destroys the turret
        turretBlue = null;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColour;
        }

        
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
