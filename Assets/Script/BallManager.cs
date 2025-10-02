using UnityEngine;

public class BallManager : MonoBehaviour
{
    // Punteggi dei giocatori
    public int ScorePlayer1 = 0;
    public int ScorePlayer2 = 0;

    // --- IMPOSTAZIONI VELOCITÀ ---
    [Header("Speed Settings")]
    [SerializeField]
    private float StartSpeed = 5f; // Velocità iniziale della palla

    [SerializeField]
    private float MaxSpeed = 15f; // Velocità massima raggiungibile

    [SerializeField]
    private float speedIncreaseOnHit = 1.1f;

    // --- IMPOSTAZIONI PUNTEGGIO ---
    [Header("Score Settings")]
    [SerializeField]
    private int AddScore = 100; // Punti assegnati per ogni goal

    [SerializeField]
    private int WinScore = 1000; // Punteggio per vincere

    // --- RIFERIMENTI ---
    [Header("References")]
    [SerializeField]
    private GameObject canvas; // Canvas da mostrare a fine partita

    private Rigidbody rigidbodyBall; // Componente Rigidbody della palla

    // --- AUDIO ---
    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clickSound;

    // --- VARIABILI PRIVATE ---
    private Vector3 BallStartPosition;
    private Quaternion BallStartRotation;
    private Vector3 currentVelocity; // Usiamo questa variabile per gestire la velocità

    void Start()
    {
        // Otteniamo il componente Rigidbody all'avvio
        rigidbodyBall = GetComponent<Rigidbody>();

        // Salviamo la posizione e rotazione iniziale per il respawn
        BallStartPosition = transform.position;
        BallStartRotation = transform.rotation;

        // Facciamo partire la palla
        RespawnBall();
    }

    void FixedUpdate()
    {
        // Limitiamo la velocità massima nel FixedUpdate per coerenza con la fisica
        // Usiamo rigidbodyBall.velocity.magnitude per ottenere la velocità reale
        if (rigidbodyBall.velocity.magnitude > MaxSpeed)
        {
            rigidbodyBall.velocity = rigidbodyBall.velocity.normalized * MaxSpeed;
        }

        // Controlliamo la condizione di vittoria
        if (ScorePlayer1 >= WinScore || ScorePlayer2 >= WinScore)
        {
            canvas.SetActive(true);
            gameObject.SetActive(false); // Disattiva la palla
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Calcoliamo la direzione del rimbalzo
        Vector3 reflection = Vector3.Reflect(currentVelocity.normalized, collision.contacts[0].normal);

        // Aumentiamo leggermente la velocità attuale
        float newSpeed = currentVelocity.magnitude;

        if (collision.gameObject.CompareTag("Player"))
        {
            newSpeed *= speedIncreaseOnHit;
        }

        // Applichiamo la nuova velocità e la nuova direzione
        currentVelocity = reflection * newSpeed;
        rigidbodyBall.velocity = currentVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerDoor1"))
        {
            ScorePlayer2 += AddScore;
            PlayClickSound();
            RespawnBall(); // La palla respawna dopo un goal
        }
        else if (other.gameObject.CompareTag("PlayerDoor2"))
        {
            ScorePlayer1 += AddScore;
            PlayClickSound();
            RespawnBall(); // La palla respawna dopo un goal
        }
    }

    // Metodo pubblico per far ripartire la palla
    public void RespawnBall()
    {
        // Ripristina posizione e rotazione
        transform.position = BallStartPosition;
        transform.rotation = BallStartRotation;

        // Ferma completamente la palla
        if (rigidbodyBall != null)
        {
            rigidbodyBall.velocity = Vector3.zero;
            rigidbodyBall.angularVelocity = Vector3.zero;
        }

        // Calcola una nuova direzione iniziale casuale (destra o sinistra)
        float randomDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        currentVelocity = new Vector3(0, 0, randomDirection * StartSpeed);

        // Applica la velocità iniziale
        rigidbodyBall.velocity = currentVelocity;
    }

    private void PlayClickSound()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}