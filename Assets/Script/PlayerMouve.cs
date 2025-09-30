using UnityEngine;

public class PlayerMouve : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 3f;

    [SerializeField]
    private int playerIndex = 0;

    private CharacterController Controller;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 inputVec = Vector3.zero;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
    }
    
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
        moveDirection = new Vector3(inputVec.x,0, inputVec.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= MoveSpeed;

        Controller.Move(moveDirection * Time.deltaTime);
    }
}
