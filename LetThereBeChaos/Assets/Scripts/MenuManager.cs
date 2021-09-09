using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("����")]
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
        SceneManager.LoadScene("�Ĥ@��");
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
