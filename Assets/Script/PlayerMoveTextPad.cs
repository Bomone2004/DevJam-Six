using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMoveTextPad : MonoBehaviour
{
    [SerializeField]
    private GameObject Player1;

    [SerializeField]
    private GameObject Player2;

    //[SerializeField]
    //private CharacterController characterController1;

    //[SerializeField]
    //private CharacterController characterController2;

    [SerializeField]
    private Rigidbody characterRigidbody1;

    [SerializeField]
    private Rigidbody characterRigidbody2;

    [SerializeField]
    private float speed = 100;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;


    private Vector3 savePlayerPosition1;
    private Vector3 savePlayerPosition2;

    private float lastDashTime = -1f;
    [SerializeField]
    private float dashCooldown = 3f; // 1 second cooldown

    [SerializeField]
    float dashSpeed = 10f;

    [SerializeField]
    private int playerIndex = 0;
    public int GetPlayerIndex()
    {
        return playerIndex;
    }


    private void Start()
    {
        savePlayerPosition1.z = Player1.transform.position.z;
        savePlayerPosition2.z = Player2.transform.position.z;
    }

    void Update()
    {
        Movement();
        DoDash();
    }

    private void Movement()
    {
        //float moveHorizontal1 = Input.GetAxis("Horizontal");
        //Vector3 movement1 = new Vector3(moveHorizontal1, 0, 0) * speed * Time.deltaTime;
        ////Player1.transform.Translate(movement1);

        ////characterController1.Move(movement1);

        //characterRigidbody1.linearVelocity = movement1;

        //// Controlla il movimento per il secondo giocatore (stick destro)
        //float moveHorizontal2 = Input.GetAxis("Horizontal2");  // Devi creare un assi personalizzato nel Input Manager
        //Vector3 movement2 = new Vector3(moveHorizontal2, 0, 0) * speed * Time.deltaTime;
        ////Player2.transform.Translate(movement2);

        ////characterController2.Move(movement2);
        //characterRigidbody2.linearVelocity = movement2;
        ///
        float moveHorizontal1 = Input.GetAxis("Horizontal");
        Vector3 movement1 = new Vector3(-moveHorizontal1, 0, 0) * speed * Time.deltaTime;
        // Muovi il player tramite Rigidbody usando MovePosition
        characterRigidbody1.MovePosition(characterRigidbody1.position + movement1);

        // Ottieni l'input orizzontale per il secondo giocatore
        float moveHorizontal2 = Input.GetAxis("Horizontal2");
        Vector3 movement2 = new Vector3(-moveHorizontal2, 0, 0) * speed * Time.deltaTime;
        // Muovi il player 2 tramite Rigidbody usando MovePosition
        characterRigidbody2.MovePosition(characterRigidbody2.position + movement2);

    }
    private void DoDash()
    {
        //float rtDash = Input.GetAxis("DashPad");

        //if (rtDash >= 1.0f && Time.time - lastDashTime >= dashCooldown)
        //{
        //    Player1.transform.position = new Vector3(Player1.transform.position.x, Player1.transform.position.y, savePlayerPosition2.z);
        //    Player2.transform.position = new Vector3(Player2.transform.position.x, Player2.transform.position.y, savePlayerPosition1.z);
        //    savePlayerPosition1.z = Player1.transform.position.z;
        //    savePlayerPosition2.z = Player2.transform.position.z;
        //    lastDashTime = Time.time;
        //    if (clickSound != null && audioSource != null)
        //    {
        //        audioSource.PlayOneShot(clickSound);
        //    }
        //}

        float rtDash = Input.GetAxis("DashPad");

        if (rtDash >= 1.0f && Time.time - lastDashTime >= dashCooldown)
        {
             // Puoi esporre questo come [SerializeField] se vuoi modificarlo da Inspector

            float startZ1 = Player1.transform.position.z;
            float startZ2 = Player2.transform.position.z;

            float targetZ1 = savePlayerPosition2.z;
            float targetZ2 = savePlayerPosition1.z;

            StartCoroutine(MoveZOverTime(Player1.transform, startZ1, targetZ1, dashSpeed));
            StartCoroutine(MoveZOverTime(Player2.transform, startZ2, targetZ2, dashSpeed));

            savePlayerPosition1.z = targetZ1;
            savePlayerPosition2.z = targetZ2;
            lastDashTime = Time.time;
            if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
        }
    }

    private System.Collections.IEnumerator MoveZOverTime(Transform playerTransform, float startZ, float targetZ, float speed)
    {
        float elapsed = 0f;
        float duration = Mathf.Abs(targetZ - startZ) / speed;
        Vector3 startPos = playerTransform.position;
        Vector3 endPos = new Vector3(startPos.x, startPos.y, targetZ);

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            playerTransform.position = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        playerTransform.position = endPos;
    }

}
