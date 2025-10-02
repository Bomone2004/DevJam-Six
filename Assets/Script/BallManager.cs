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

    [SerializeField]
    private int AddScore = 100;
    private Rigidbody rigidbodyBall;
    [SerializeField] GameObject canvas;

    [SerializeField] int WinScoor = 1000;

    [SerializeField] float MaxSpeed = 3;
    [SerializeField] float MinSpeed = 0.5f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;





    private Vector3 BallStartPosition = Vector3.zero;

    void Start()
    {
        rigidbodyBall = GetComponent<Rigidbody>();
        BallStartPosition = transform.position;
        //int takerandomRange = Random.RandomRange(0, 2);
        //if (takerandomRange == 0)
        //{
        //    speed = new Vector3(0, 0, StartSpeed);
        //}
        //if (takerandomRange == 1)
        //{
        //    speed = new Vector3(0, 0, -StartSpeed);
        //}
        SetInitialSpeed();


    }

    void Update()
    {
        // Decelerazione sull'asse Z
        if (Mathf.Abs(speed.z) > 1)
        {
            float sign = Mathf.Sign(speed.z);
            speed.z -= deceleration * Time.deltaTime * sign;
            if (Mathf.Abs(speed.z) < 1)
            {
                speed.z = sign;
            }
        }

        // Limita la velocità massima
        speed = ClampVector3Magnitude(speed, MaxSpeed);

        Vector3 movement1 = speed * Time.deltaTime;
        rigidbodyBall.MovePosition(rigidbodyBall.position + movement1);

        Debug.LogWarning(movement1);


        if (ScorePlayer1 == WinScoor ||  ScorePlayer2 == WinScoor)
        {
            canvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("WallX"))
        {
            speed.x *= -1;
        }
        else if (collision.gameObject.CompareTag("WallZ"))
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerDoor1"))
        {
            ScorePlayer1 += AddScore;
            if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
        }
        else if (collision.gameObject.CompareTag("PlayerDoor2"))
        {
            ScorePlayer2 += AddScore;
            if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
        }
        else if (collision.gameObject.CompareTag("PlayerDoorRespawn"))
        {
            transform.position = BallStartPosition;
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

    private Vector3 ClampVector3Magnitude(Vector3 v, float maxMagnitude)
    {
        if (v.magnitude > maxMagnitude)
            return v.normalized * maxMagnitude;
        return v;
    }

    // Imposta la velocità iniziale in modo casuale
    private void SetInitialSpeed()
    {
        int takerandomRange = Random.Range(0, 2);
        if (takerandomRange == 0)
        {
            speed = new Vector3(0, 0, StartSpeed);
        }
        else
        {
            speed = new Vector3(0, 0, -StartSpeed);
        }
    }

}
