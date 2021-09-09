using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("音效")]
    public AudioClip soundClick;

    private AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        aud.PlayOneShot(soundClick);
        Invoke("DelayStartGame", 1);
    }

    private void DelayStartGame()
    {
        SceneManager.LoadScene("第一關");
    }

    public void QuitGame()
    {
        aud.PlayOneShot(soundClick);
        Invoke("DelayQuit", 1);
    }

    private void DelayQuit()
    {
        Application.Quit();
    }
}
