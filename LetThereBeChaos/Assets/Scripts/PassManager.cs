using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PassManager : MonoBehaviour
{
    [Header("碰到後會過關的物件名稱")]
    public string namePass = "過關區域";
    [Header("延遲通關")]
    public float delay = 0.5f;
    [Header("音效")]
    public AudioClip soundPass;

    private AudioSource aud;
    /// <summary>
    /// 結束畫面
    /// </summary>
    private CanvasGroup groupFinal;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        groupFinal = GameObject.Find("結束畫面").GetComponent<CanvasGroup>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == namePass) Pass();
    }

    /// <summary>
    /// 過關
    /// </summary>
    private void Pass()
    {
        aud.PlayOneShot(soundPass);

        int levelCount = SceneManager.GetActiveScene().buildIndex;

        if (levelCount == 6)
        {
            StartCoroutine(ShowFinal());
        }
        else
        {
            Invoke("DelayPass", delay);
        }

        // 關閉上下視角控制器
        GetComponent<TopDownController>().Stop();
    }

    /// <summary>
    /// 延遲通關
    /// </summary>
    private void DelayPass()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// 顯示結束畫面
    /// </summary>
    private IEnumerator ShowFinal()
    {
        float count = 10;

        for (int i = 0; i < count; i++)
        {
            print(123);
            groupFinal.alpha += 1f / count;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
