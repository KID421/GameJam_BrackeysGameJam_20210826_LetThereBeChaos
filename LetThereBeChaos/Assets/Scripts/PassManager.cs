using UnityEngine;
using UnityEngine.SceneManagement;

public class PassManager : MonoBehaviour
{
    [Header("碰到後會過關的物件名稱")]
    public string namePass = "過關區域";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == namePass) Pass();
    }

    /// <summary>
    /// 過關
    /// </summary>
    private void Pass()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(index);
    }
}
