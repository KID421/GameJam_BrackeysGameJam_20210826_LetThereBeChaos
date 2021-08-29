using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("����ɶ�")]
    public float delayTime = 1.5f;

    public void Replay()
    {
        Invoke("DelayReplay", delayTime);
    }

    private void DelayReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
