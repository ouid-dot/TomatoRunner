using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public float speed = 5;
    public float xDir = 0;
    public int groundContacts = 0;
    public float jumpForce = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * xDir * speed * Time.deltaTime);
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        xDir = ctx.ReadValue<Vector2>().x;


    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started && groundContacts > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * jumpForce);

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            groundContacts++;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            groundContacts--;
        }
    }

}

