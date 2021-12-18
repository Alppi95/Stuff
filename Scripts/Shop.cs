using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint gatlingTurret;
    public TurretBluePrint missileTurret;
    public TurretBluePrint laserTurret;
    public TurretBluePrint aaTurret;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectGatlingTurret()
    {
        buildManager.SelectTurretToBuild(gatlingTurret);
    }

    public void SelectMissileTurret()
    {
        buildManager.SelectTurretToBuild(missileTurret);
    }

    public void SelectLaserTurret()
    {
        buildManager.SelectTurretToBuild(laserTurret);
    }

    public void SelectAATurret()
    {
        buildManager.SelectTurretToBuild(aaTurret);
    }
 


}
