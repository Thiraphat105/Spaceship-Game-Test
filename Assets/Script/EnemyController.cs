using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
