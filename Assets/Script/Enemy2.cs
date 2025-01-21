using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 2f; // ความเร็วของศัตรู
    public GameObject missile; // กระสุนที่ศัตรูยิง
    public Transform missileSpawnPoint; // ตำแหน่งที่กระสุนเกิด
    public float fireRate = 2f; // ความถี่ในการยิง (วินาที)
    public float missileDestroyTime = 5f; // เวลาที่กระสุนจะถูกทำลาย
    public float spreadAngle = 45f; // มุมกระจายของกระสุน

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

            // ยิงกระสุน 4 ลูกด้วยมุมที่แตกต่างกัน
            for (int i = 0; i < 4; i++)
            {
                // คำนวณมุมการหมุนของกระสุน
                float angle = -spreadAngle / 2 + (spreadAngle / 3) * i;
                Quaternion rotation = Quaternion.Euler(0, 0, angle);

                // สร้างกระสุน
                GameObject missileInstance = Instantiate(missile, missileSpawnPoint.position, rotation);

                // กระสุนทำลายตัวเองหลังจากเวลาที่กำหนด
                Destroy(missileInstance, missileDestroyTime);
            }
        }
    }
}
