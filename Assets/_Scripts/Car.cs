using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip carAlarmClip;
    public GameObject[] carAlarmLights;
    private bool isCarAlarmActive;
    private Coroutine carAlarmCoRoutine;
    private float timer;
    private void Start()
    {
        isCarAlarmActive = false;
    }
    
    private void Update()
    {
        if(isCarAlarmActive)
        {
            timer += Time.deltaTime;
            if(timer >= 60)
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
            foreach (GameObject obj in carAlarmLights)
                obj.SetActive(lightsOn);
            //audioSource.PlayOneShot(carAlarmClip);
            lightsOn = !lightsOn;
            yield return new WaitForSeconds(1f);
            
        }
        yield return null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cart")
        {
            GameManager.instance.updateDamageScore();
            if (carAlarmCoRoutine == null)
                carAlarmCoRoutine = StartCoroutine(PlayCarAlarm());
        }
    }
}
