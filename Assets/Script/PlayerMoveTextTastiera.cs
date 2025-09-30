using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveTextTastiera : MonoBehaviour
{
    [SerializeField]
    private GameObject Player1;

    [SerializeField]
    private GameObject Player2;

    [SerializeField]
    private float speed = 3;

    //[SerializeField]
    private PlayerInput playerInput;

    private Vector3 inputVec = Vector3.zero;

    [SerializeField]
    private int playerIndex = 0;
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
        Movement();

    }

    private void Movement()
    {
        float moveHorizontal1 = Input.GetAxis("Tatiera Horizontal 1");
        Vector3 movement1 = new Vector3(moveHorizontal1, 0, 0) * speed * Time.deltaTime;
        Player1.transform.Translate(movement1);

        // Controlla il movimento per il secondo giocatore (stick destro)
        float moveHorizontal2 = Input.GetAxis("Tastiera Horizontal 2");  // Devi creare un assi personalizzato nel Input Manager
        Vector3 movement2 = new Vector3(moveHorizontal2, 0, 0) * speed * Time.deltaTime;
        Player2.transform.Translate(movement2);
    }

}
