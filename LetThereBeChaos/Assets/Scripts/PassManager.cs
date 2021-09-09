using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PassManager : MonoBehaviour
{
    [Header("�I���|�L��������W��")]
    public string namePass = "�L���ϰ�";
    [Header("����q��")]
    public float delay = 0.5f;
    [Header("����")]
    public AudioClip soundPass;

    private AudioSource aud;
    /// <summary>
    /// �����e��
    /// </summary>
    private CanvasGroup groupFinal;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        groupFinal = GameObject.Find("�����e��").GetComponent<CanvasGroup>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == namePass) Pass();
    }

    /// <summary>
    /// �L��
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

        // �����W�U�������
        GetComponent<TopDownController>().Stop();
    }

    /// <summary>
    /// ����q��
    /// </summary>
    private void DelayPass()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// ��ܵ����e��
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
