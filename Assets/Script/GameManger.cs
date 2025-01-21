using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Item Settings")]
    public GameObject[] itemPrefabs; // Prefab ของไอเท็ม
    public float minX = -10f, maxX = 10f; // ขอบเขตการสุ่มแกน X
    public float minY = -5f, maxY = 5f;   // ขอบเขตการสุ่มแกน Y
    public float spawnInterval = 5f; // ระยะเวลาการเกิดไอเท็มใหม่

    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs; // Prefabs ของศัตรู
    public float minInstantiateValue = -10f; // ค่าต่ำสุดของการสุ่มตำแหน่งศัตรู
    public float maxInstantiateValue = 10f;  // ค่าสูงสุดของการสุ่มตำแหน่งศัตรู
    public float enemyDestroyTime = 3f; // เวลาที่ศัตรูจะถูกทำลาย

    [Header("Game Settings")]
    public GameOverUi GameOverUi; // UI สำหรับ Game Over
    private int maxPlatform = 0; // ตัวแปรสำหรับนับจำนวนแพลตฟอร์ม
    private bool isPlayerAlive = true; // ตัวแปรเพื่อตรวจสอบสถานะผู้เล่น

    private void Start()
    {
        isPlayerAlive = true; // ตั้งค่าเริ่มต้น
        InvokeRepeating(nameof(InstantiateEnemy), 1f, 1f); // เรียกใช้ฟังก์ชัน InstantiateEnemy ทุกๆ 1 วินาที
        InvokeRepeating(nameof(SpawnItem), 2f, spawnInterval); // เรียกใช้ฟังก์ชัน SpawnItem ทุกๆ spawnInterval วินาที
    }

    public void GameOver()
    {
        isPlayerAlive = false; // หยุดสถานะผู้เล่น
        CancelInvoke(nameof(InstantiateEnemy)); // ยกเลิกการ Spawn ศัตรู
        CancelInvoke(nameof(SpawnItem)); // ยกเลิกการ Spawn ไอเท็ม

        int bonus = Mathf.FloorToInt(Time.time * 50); // คำนวณโบนัส (เช่น 50 คะแนนต่อวินาที)
        GameOverUi.Setup(maxPlatform, bonus); // ส่ง maxPlatform และ bonus ไปที่ GameOverUi
    }

    void InstantiateEnemy()
    {
        if (!isPlayerAlive) return; // ตรวจสอบสถานะก่อนสร้างศัตรู

        Vector3 enemyPos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 5.6f);
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedEnemyPrefab = enemyPrefabs[randomIndex];
        GameObject enemyInstance = Instantiate(selectedEnemyPrefab, enemyPos, Quaternion.identity);
        Destroy(enemyInstance, enemyDestroyTime); // ทำลายศัตรูหลังจากเวลาที่กำหนด
    }

    void SpawnItem()
    {
        if (itemPrefabs == null || itemPrefabs.Length == 0)
        {
            Debug.LogWarning("Item Prefabs are not assigned or empty!");
            return;
        }

        int randomIndex = Random.Range(0, itemPrefabs.Length);
        GameObject item = itemPrefabs[randomIndex]; // เลือกไอเท็มแบบสุ่ม
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        Instantiate(item, spawnPosition, Quaternion.identity); // สร้างไอเท็ม
    }
    
}
