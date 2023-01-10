using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == "EnergyDrink")
        {
            target.GetComponent<Bonus>().HiddenObject();
            GameManager.Instance.SetActiveBonus("EnergyDrink");
        }
        else if(target.tag == "Magnesia")
        {
            target.GetComponent<Bonus>().HiddenObject();
            GameManager.Instance.SetActiveBonus("Magnesia");
        }
    }
}
