using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using TMPro; 
public class InputManager : NetworkBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] TMP_Text jumpCount;
    [SyncVar] public int saltos = 0; 

    private Vector3 direction;
    private bool isGrounded;

    private void Start()
    {
        jumpCount = GameObject.FindWithTag("JumpText").GetComponent<TMP_Text>(); 

        if (isLocalPlayer)
        {
            GetComponent<PlayerInput>().enabled = true; 
        }

        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        jumpCount.text = saltos.ToString(); 
        rb.MovePosition(rb.position + (direction * speed * Time.deltaTime));  
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        if (!isLocalPlayer) return;
        Vector2 input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0, input.y);
    }

    public void Jump(InputAction.CallbackContext callBackContext)
    {
        if (!isLocalPlayer) return;
        if (callBackContext.performed && isGrounded == true)
        {
            saltos++; 
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            Debug.Log("tocando suelo"); 
        }
    }
}
