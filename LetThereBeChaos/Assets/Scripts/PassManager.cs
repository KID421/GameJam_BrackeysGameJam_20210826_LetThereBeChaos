using UnityEngine;
using UnityEngine.SceneManagement;

public class PassManager : MonoBehaviour
{
    [Header("�I���|�L��������W��")]
    public string namePass = "�L���ϰ�";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == namePass) Pass();
    }

    /// <summary>
    /// �L��
    /// </summary>
    private void Pass()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(index);
    }
}
