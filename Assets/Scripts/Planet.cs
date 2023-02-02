using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Planet : MonoBehaviour
{
    [SerializeField]  GameObject planet;

    private AudioSource explosionSound;
    private Animator animator;
    private bool destroyed = false;
    private bool toRemove = false;

    void Start()
    {
        StartCoroutine(MainCoroutine());
        explosionSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    IEnumerator MainCoroutine()
    {
        while(true)
        {
            if (transform.position.y < 0)
            {
                animator.SetTrigger("Escape");
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void PlayExplosionSound()
    {
        explosionSound.Play();
    }

    public void KillPlanet()
    {
        planet.SetActive(false);
        destroyed = true;
        toRemove = true;
    }
    public void ActiveExplosion()
    {
        destroyed = true;
        animator.SetTrigger("Explosion");
    }

    public bool GetDestructionStatus()
    {
        return destroyed;
    }
    public bool GetRemoveStatus()
    {
        return toRemove;
    }

}

