using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUi : MonoBehaviour
{
    public Text pointsText;
    public Text bonusText; // ข้อความแสดงโบนัส

    public void Setup(int totalScore, int bonus)
{
    gameObject.SetActive(true);
    pointsText.text = $"Total Score: {totalScore} POINTS";
    bonusText.text = $"Time Bonus: {bonus} POINTS";
}

    public void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("SceneMainMenu");
    }
}
