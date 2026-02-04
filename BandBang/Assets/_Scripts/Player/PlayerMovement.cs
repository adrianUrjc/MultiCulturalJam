using UnityEngine;
public class PlayerMovement : MonoBehaviour
{

    [SerializeField, ReadOnly] public Vector2 moveInput;
   // public Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField] private Transform interactionObject;

    private void Awake()
    {
       // rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position+ (new Vector2(moveInput.x, 0/*moveInput.y*/) * Time.fixedDeltaTime * moveSpeed));

        if (moveInput.sqrMagnitude > 0.1f)
        {
          
            //change de z axis of rotation to match movement direction for the interaction object
            float zAngle = Mathf.Atan2(-moveInput.x, moveInput.y) * Mathf.Rad2Deg;
           
            interactionObject.rotation =Quaternion.Euler(0,0, zAngle);


        }
    }
  

}
