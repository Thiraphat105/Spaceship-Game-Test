using UnityEngine;

public class Enemy1Control : MonoBehaviour
{
    public float speed = 2f; // ความเร็วของศัตรู
    public GameObject missile; // กระสุนที่ศัตรูยิง
    public Transform missileSpawnPoint; // ตำแหน่งที่กระสุนเกิด
    public float fireRate = 2f; // ความถี่ในการยิง (วินาที)
    public float missileDestroyTime = 5f; // เวลาที่กระสุนจะถูกทำลาย

    private float nextFireTime = 0f;

    void Update()
    {
        MoveEnemy();
        FireMissile();
    }

    void MoveEnemy()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime); // ศัตรูเคลื่อนที่ลงด้านล่าง
    }

    void FireMissile()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // ตั้งเวลาครั้งถัดไปที่จะยิง
            GameObject missileInstance = Instantiate(missile, missileSpawnPoint.position, Quaternion.identity);

            // กระสุนทำลายตัวเองหลังจากเวลาที่กำหนด
            Destroy(missileInstance, missileDestroyTime);
        }
    }
    
}
