using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerMainManu : MonoBehaviour
{
    // ฟังก์ชันเริ่มเกม
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");  // เปลี่ยนไปที่ฉากเกม
    }

    // ฟังก์ชันออกจากเกม
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!");  // ใช้ Debug สำหรับตรวจสอบเวลาเล่นใน Editor
    }
}
