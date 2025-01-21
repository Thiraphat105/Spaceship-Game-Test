using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float missilespeed = 25f;
    void Update()
    {
        transform.Translate(Vector3.up * missilespeed * Time.deltaTime);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

}
