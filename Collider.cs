using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Collider : MonoBehaviour
{
    [SerializeField] AudioClip FinSound;
    [SerializeField] ParticleSystem PlayerBoom;
    bool IsMeetOther = false;
    AudioSource audioSource;

    [SerializeField] float DelayTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerCrack()
    {
        PlaySound();
        PlayerBoom.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Destroy(GetComponent<PlayerController>());
        Invoke("reLoadScene",DelayTime);
    }
    void PlaySound()
    {   
        audioSource.Stop();
        audioSource.PlayOneShot(FinSound);

    }
    void reLoadScene()
    {
        int CurrentSceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneNumber);
        
    }


private void OnCollisionEnter(Collision other) 
    {
        if (IsMeetOther) { return; }
        switch(other.gameObject.tag)
            {
            case "Enemy":
                Debug.Log("enemy here");
                PlayerCrack();
                break;
            case "fuel":
                    break;
        }
    }
}
