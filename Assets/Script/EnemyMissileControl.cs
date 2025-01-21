using UnityEngine;

public class EnemyMissileControl : MonoBehaviour
{
    public float missileSpeed = 25f;
    

    void Update()
    {
        transform.Translate(Vector3.down * missileSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision detected with: {collision.gameObject.name}");

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(1);
                Debug.Log("Player took damage!");
            }

            Destroy(gameObject);
        }
    }
}
