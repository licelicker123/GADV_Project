using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTrigger2D : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource soundEffect;
    public float soundRange = 10f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            soundEffect.Play();

            GameObject enemy = GameObject.FindWithTag("Enemy");
            if (enemy != null)
            {
                enemy.GetComponent<DirectorAI>().HearSound(transform.position);
            }
        }
    }
}
