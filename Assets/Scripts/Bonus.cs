using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public GameObject obj;
    public AudioSource activationSound;

    public void HiddenObject()
    {
        StartCoroutine(WaitForTheSound());  
    }
    
    IEnumerator WaitForTheSound()
    {
        activationSound.Play();
        yield return new WaitForSeconds(0.5f);
        obj.SetActive(false);
    }
}
