using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int totalScore = 0; // คะแนนรวมทั้งหมด
    public int health = 5; // พลังชีวิตของผู้เล่น
    public Text healthText; // UI แสดงพลังชีวิตและคะแนน
    public GameOverUi gameOverUi; // UI เมื่อเกมจบ

    public float speed = 10f; // ความเร็วของผู้เล่น
    public float destroyTime = 1f; // เวลาที่กระสุนจะถูกทำลาย
    public GameObject missile; // กระสุนของผู้เล่น
    public Transform missileSpawn; // ตำแหน่งที่กระสุนถูกยิงออก

    private bool isDoubleShootActive = false; // สถานะยิงกระสุนคู่
    private bool isBigMissileActive = false;  // สถานะยิงกระสุนใหญ่
    private float shootCooldown = 0.5f; // ระยะเวลาระหว่างการยิง
    private float shootTimer = 0f; // ตัวจับเวลาการยิง

    private void Start()
    {
        UpdateHealthUI(); // อัปเดต UI ตอนเริ่มต้น
    }

    private void Update()
    {
        PlayerMovement(); // การเคลื่อนที่ของผู้เล่น
        PlayerShoot();    // การยิงกระสุน
    }

    // การเคลื่อนที่ของผู้เล่น
    void PlayerMovement()
    {
        float xpos = Input.GetAxis("Horizontal");
        float ypos = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xpos, ypos, 0) * speed * Time.deltaTime;

        transform.Translate(movement);
    }

    // การยิงกระสุน
    void PlayerShoot()
    {
        shootTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && shootTimer >= shootCooldown)
        {
            shootTimer = 0f;

            if (isDoubleShootActive)
            {
                InstantiateMissile(Vector3.left * 0.5f);
                InstantiateMissile(Vector3.right * 0.5f);
            }
            else if (isBigMissileActive)
            {
                InstantiateBigMissile();
            }
            else
            {
                InstantiateMissile(Vector3.zero);
            }
        }
    }

    void InstantiateMissile(Vector3 offset)
    {
        GameObject gm = Instantiate(missile, missileSpawn.position + offset, Quaternion.identity);
        Destroy(gm, destroyTime);
    }

    void InstantiateBigMissile()
    {
        GameObject bigMissile = Instantiate(missile, missileSpawn.position, Quaternion.identity);
        bigMissile.transform.localScale *= 1.5f;
        Destroy(bigMissile, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1); // ลดพลังชีวิตเมื่อชนศัตรู
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            var itemComponent = collision.GetComponent<Item>();
            if (itemComponent != null)
            {
                int itemType = itemComponent.itemType;

                if (itemType == 1)
                {
                    totalScore += 20;
                    ActivateDoubleShoot();
                }
                else if (itemType == 2)
                {
                    totalScore += 30;
                    ActivateBigMissile();
                }

                UpdateHealthUI();
                Destroy(collision.gameObject);
            }
        }
    }

    void ActivateDoubleShoot()
    {
        isDoubleShootActive = true;
        isBigMissileActive = false;
        shootCooldown = 0.3f;
    }

    void ActivateBigMissile()
    {
        isBigMissileActive = true;
        isDoubleShootActive = false;
        shootCooldown = 0.7f;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthUI();

        if (health <= 0)
        {
            int bonus = Mathf.FloorToInt(Time.time * 50);
            totalScore += bonus;

            if (gameOverUi != null)
            {
                gameOverUi.Setup(totalScore, bonus);
            }

            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {health} | Score: {totalScore}";
        }
    }
}
