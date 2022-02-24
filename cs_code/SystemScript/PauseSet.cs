using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSet : MonoBehaviour
{
    public GameObject pauseUI;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseUI.activeSelf)
        {
            audio.Pause();
            Time.timeScale = 0;
            pauseUI.SetActive(!pauseUI.activeSelf);

        }
    }
}
