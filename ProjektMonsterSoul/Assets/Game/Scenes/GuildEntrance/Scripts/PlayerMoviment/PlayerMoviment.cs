using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoviment : MonoBehaviour
{
    [SerializeField]private float movespeed;
    private Rigidbody2D playerrb;
    private Vector2 playermoviment;
    private Animator playeranimator;
    void Start()
    {
        playerrb = GetComponent<Rigidbody2D>();
        playeranimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        playerrb.linearVelocity = playermoviment * movespeed;
        Animations();
    }

    private void Animations()
    {
        playeranimator.SetFloat("X", playermoviment.x);
        playeranimator.SetFloat("Y", playermoviment.y);
        if (playermoviment.x != 0 || playermoviment.y != 0)
        {
            playeranimator.SetBool("Idle", false);
        }else playeranimator.SetBool("Idle", true);
            
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        playermoviment = context.ReadValue<Vector2>();
        
    }
}
