using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguiser : MonoBehaviour
{
    [SerializeField]
    private Transform parentGo;
    private ParticleSystem rain1;
    private ParticleSystem rain2;

    [SerializeField] 
    private ParticleSystem fire;

    [SerializeField]
    private LaserInterper interperter;
    
    [SerializeField]
    private Lasers lasers;

    [SerializeField]
    private float fireDelay;

    [SerializeField]
    private List<Interactable> doors;

    bool beenFired = false;
    private void Awake()
    {
        rain1 = parentGo.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        rain2 = parentGo.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        
        parentGo.gameObject.SetActive(false);
        fire.Stop();

        EventManager.Instance.onTriggerSolved += StartFire;
        EventManager.Instance.onTriggerSolved += StartRain;
    }
    
    IEnumerator StopRain()
    {
        parentGo.gameObject.SetActive(true);
        rain1.Play();
        rain2.Play();
        yield return new WaitForSeconds(3f);
        fire.Stop();
        yield return new WaitForSeconds(5f);
        parentGo.gameObject.SetActive(false);
    }

    IEnumerator StartFire(float time )
    {
        yield return new WaitForSeconds(time);
        fire.Play();
        
    }

    private void StartRain(IPuzzleComponent puzzle)
    {
        if (puzzle == interperter)
        {
            StartCoroutine(StopRain());
        }
    }
    private void StartFire(IPuzzleComponent puzzle)
    {
        if (puzzle == lasers)
        {
            StartCoroutine(StartFire(fireDelay));
        }
    }

    void LockDoors()
    {
        foreach (var door in doors)
        {
            door.enabled = false;
        }  
    }
}
