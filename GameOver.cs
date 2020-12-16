using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public AudioClip audioGameOver;
    private AudioSource MPGameOver;

    // Start is called before the first frame update
    void Start()
    {

        MPGameOver = gameObject.AddComponent<AudioSource>();
        MPGameOver.clip = audioGameOver;

        MPGameOver.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Replay()
    {
        Coins.score = 0;
        SceneManager.LoadScene(Coins.scene);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
