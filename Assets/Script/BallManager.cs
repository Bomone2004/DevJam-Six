using UnityEngine;

public class BallManager : MonoBehaviour
{
    public int ScorePlayer1 = 0;
    public int ScorePlayer2 = 0;

    [SerializeField]
    private float StartSpeed = 1;

    [SerializeField]
    private Vector3 speed = Vector3.zero;

    [SerializeField]
    float deceleration;

    private Rigidbody rigidbodyBall;




    private Vector3 BallStartPosition = Vector3.zero;

    void Start()
    {
        rigidbodyBall = GetComponent<Rigidbody>();
        BallStartPosition = transform.position;
        int takerandomRange = Random.RandomRange(0, 2);
        if (takerandomRange == 0)
        {
            speed = new Vector3(0, 0, StartSpeed);
        }
        if (takerandomRange == 1)
        {
            speed = new Vector3(0, 0, -StartSpeed);
        }
        
    }

    void Update()
    {
        Vector3 movement1 = speed * Time.deltaTime;
        if (speed.z > 1)
        {
            speed.z -= deceleration *Time.deltaTime;
            if (speed.z < 1)
            {
                speed.z = 1;
            }
            if (speed.x > 10)
            {
                speed.z = 10;
            }
        }
        //transform.Translate(movement1);
        rigidbodyBall.MovePosition(rigidbodyBall.position + movement1);

        Debug.LogWarning(movement1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerDoor1"))
        {
            ScorePlayer1 += 100;
            transform.position = BallStartPosition;
        }
        if (collision.gameObject.CompareTag("PlayerDoor2"))
        {
            ScorePlayer2 += 100;
            transform.position = BallStartPosition;
        }
        if (collision.gameObject.CompareTag("WallX"))
        {
            speed.x *= -1;
        }
        if (collision.gameObject.CompareTag("WallZ"))
        {
            speed.z *= -1;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Use the collision normal to reflect the speed vector for a realistic bounce
            if (collision.contacts.Length > 0)
            {
                Vector3 normal = collision.contacts[0].normal;
                speed = Vector3.Reflect(speed * 2, normal);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("WallX"))
        {
            speed.x *= -1;
        }
        if (collision.gameObject.CompareTag("WallY"))
        {
            speed.z *= -1;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Use the collision normal to reflect the speed vector for a realistic bounce
            if (collision.contacts.Length > 0)
            {
                Vector3 normal = collision.contacts[0].normal;
                speed = Vector3.Reflect(speed, normal);
            }
        }
    }
}
