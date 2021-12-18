using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public Text upgradeCost;
    public Button upgradeButton;
    public Text sellValue;

    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosition(); //sets the tranform to match the build position

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlue.upgradeCost;        //updates the upgrade cost
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }
        sellValue.text = "$" + target.turretBlue.GetSellAmount();
        ui.SetActive(true); //set sell/upgrade ui active
    }

    public void HideUI()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        //upgrades turret
        target.UpgradeTurret();
        //hides the node highlight
        BuildManager.instance.DeselectNode();
    }

    public void SellTurret()
    {
        //sell turret
        target.SellTurret();
        //deselect node
        BuildManager.instance.DeselectNode();
    }

}
