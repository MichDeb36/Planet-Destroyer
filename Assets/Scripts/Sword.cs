using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;


public class Sword : MonoBehaviour
{
    [Range (0, 1)]
    public float intensity;
    public float duration;
    public XRBaseController controller;
    public GameObject sword;

    private float bonusEnergyDrink;
    private float bonusMagnesia;
    private float speedCriterion;
    private Vector3 oldPosition;
    private GameObject planet;
    private int score = 0;
    private int numberDestroyPlanet = 0;
    private int IdObject = 0;
    private int rescalingPoints = 100;
    private string bonus;

    void Start()
    {
        bonusEnergyDrink = GameManager.Instance.GetBonusEnergyDrink();
        bonusMagnesia = GameManager.Instance.GetBonusMagnesia();
        speedCriterion = GameManager.Instance.GetSpeedCriterion();
        bonus = GameManager.Instance.GetActiveBonus();
        if(bonus == "Magnesia") // sword magnification, precision effect
        {
            sword.transform.localScale = new Vector3(bonusMagnesia, bonusMagnesia, bonusMagnesia);
        }
    }

    /*checking whether the object is a planet,
               whether it has not been hit before,
               whether it has not been previously destroyed,
               whether the impact had a minimum value of the velocity criterion*/
    public void pointsCalculation(Collider target)
    {
        if (target.tag == "Planet" && IdObject != target.GetInstanceID())
        {
            if(!target.GetComponent<Planet>().GetDestructionStatus())
            {
                float v = Vector3.Magnitude(this.transform.position - oldPosition) * 100;
                if (v > speedCriterion)
                {
                    target.GetComponent<Planet>().activeExplosion();
                    IdObject = target.GetInstanceID();
                    numberDestroyPlanet++;
                    if (bonus == "EnergyDrink") //point multiplier bonusEnergyDrink, force effect
                    {
                        score += (int)(v * rescalingPoints * bonusEnergyDrink);
                    }
                    else
                        score += (int)(v * rescalingPoints);
                }
            }
        }
    }


    private void OnTriggerEnter(Collider target)
    {
        pointsCalculation(target);
        SendHaptic(controller);
    }

    private void SendHaptic(XRBaseController controller)
    {
        controller.SendHapticImpulse(intensity, duration);
    }

    public int GetScore()
    {
        return score;
    }
    public int GetNumberDestroyPlanet()
    {
        return numberDestroyPlanet;
    }
    public void ResetScore()
    {
        score = 0;
    }
    public void ResetNumberDestroyPlanet()
    {
        numberDestroyPlanet = 0;
    }

    void Update()
    {
        oldPosition = this.transform.position;
    }
}
