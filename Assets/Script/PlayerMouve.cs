using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 5f;

    [SerializeField]
    private int playerIndex = 0;

    private Vector3 inputVec = Vector3.zero;

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVec = direction;
    }

    void Update()
    {
        // Considera solo l'input sull'asse orizzontale (X).
        float moveInput = inputVec.x;

        // Crea un vettore di direzione solo orizzontale.
        Vector3 moveDirection = Vector3.right * moveInput;

        // Applica il movimento direttamente al Transform.
        transform.Translate(moveDirection * MoveSpeed * Time.deltaTime);
    }
}

