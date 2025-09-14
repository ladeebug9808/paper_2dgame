using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Settings")]
    public GameObject playerPrefab;
    public GameObject spawnPoint;

    [Header("Respawn Settings")]
    public float respawnYThreshold = -10f;
    public float respawnDelay = 1.5f; // seconds to wait after fade out

    public float playerDeaths;

    public float maxLives = 5f;

    [Header("References")]
    public FadeController fadeController;
    public CanvasGroup canvasGroup;
    public AudioListener audioListener;

    private GameObject currentPlayer;
    private Rigidbody playerRB;
    private bool isRespawning = false;
    private bool ranOutOfLives;

    void Start()
    {
        SpawnPlayer();
        audioListener = FindObjectOfType<AudioListener>();
    }

    void SpawnPlayer()
    {
        currentPlayer = Instantiate(playerPrefab, spawnPoint.transform.position, playerPrefab.transform.rotation, transform);
        playerRB = currentPlayer.GetComponentInChildren<Rigidbody>();
    }

    void Update()
    {
        if (playerRB.transform.position.y < respawnYThreshold)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
         if (!isRespawning && playerRB != null && !ranOutOfLives)
        {
            StartCoroutine(RespawnSequence());
        }
    }

    IEnumerator RespawnSequence()
    {

        if (playerDeaths < maxLives)
        {
            playerDeaths += 1;
        }
        else
        {
            yourOutOfLives();
            yield break;
        }
        

        
        isRespawning = true;

        


        fadeController.FadeOut();


        yield return new WaitForSeconds(respawnDelay);



        Destroy(currentPlayer);


        SpawnPlayer();
        audioListener = FindObjectOfType<AudioListener>();
        fadeController.FadeIn();
        

        isRespawning = false;
    }

    public void yourOutOfLives()
    {
        ranOutOfLives = true;
        //print("its wraps gng");
        canvasGroup.alpha = 1;
        audioListener.enabled = false;
    }
}
