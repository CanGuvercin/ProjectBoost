using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float DelayerMaster = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisabled) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("tamam dost bolgedesin");
                break;
            case "Finish":
                Debug.Log("tamam bitis bolgesindesin");
                LoadNextLevelSceneMaster();
                break;
            case "fuel":
                Debug.Log("tamam simdi yakýt aldýn kereta");
                break;
            default:
                Debug.Log("batýrdýn");
                //SceneManager.LoadScene("SandBox");
                StartCrachScene(); 
                break;
        }
    }
    void StartCrachScene()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
    }

    void LoadNextLevelSceneMaster()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", DelayerMaster);
        
        
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+1;
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene("Sandbox2");
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
