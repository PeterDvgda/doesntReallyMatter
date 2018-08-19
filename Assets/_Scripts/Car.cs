using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip carAlarmClip;
    public GameObject[] carAlarmLights;
    private bool isCarAlarmActive;
    private Coroutine carAlarmCoroutine;
    private float timer;
    private bool isHit;
    private void Start()
    {
        isCarAlarmActive = false;
        isHit = false;
    }
    
    private void Update()
    {
        if(isCarAlarmActive)
        {
            timer += Time.deltaTime;
            if(timer >= 20)
            {
                isCarAlarmActive = false;
                timer = 0;
            }
        }
    }
    IEnumerator PlayCarAlarm()
    {
        isCarAlarmActive = true;
        bool lightsOn = true;
        while(isCarAlarmActive)
        {
            if (lightsOn == true)
                audioSource.PlayOneShot(carAlarmClip);
            foreach (GameObject obj in carAlarmLights)
                obj.SetActive(lightsOn);
            lightsOn = !lightsOn;
            yield return new WaitForSeconds(0.5f);
            

        }
        carAlarmCoroutine = null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cart")
        {
            if(isHit == true)
                GameManager.instance.updateDamageScore();
            AudioManager.instance.PlayOneShotCarImpact();
            if (carAlarmCoroutine == null)
                carAlarmCoroutine = StartCoroutine(PlayCarAlarm());
        }
    }
}
