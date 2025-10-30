using UnityEngine;

public class PlayerMovement : MonoBehaviour,IDamagable
{
    public float sprintSpeed;
    public float walkSpeed;
    public float jumpForce;
    public Camera playerCamera;
    private Rigidbody rigidBody;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float friction = 10;
    [SerializeField] private float gravity = 3;
    public static PlayerMovement Instance;
    public float externalSpeedMultiplier = 1f;
    private bool isGrounded = false;
    private bool sprintMode = true;

    void Awake(){
        Instance = this;
    }
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float horzMove = Input.GetAxisRaw("Horizontal");
        float vertMove = Input.GetAxisRaw("Vertical");
        moveDirection = (transform.forward * vertMove + transform.right * horzMove).normalized;
        
        if(Input.GetKey(KeyCode.Space) && isGrounded){
            rigidBody.AddForce(transform.up * jumpForce,ForceMode.Impulse);
            isGrounded = false;
        }
        if(Input.GetKeyDown(KeyCode.B)){
            sprintMode = !sprintMode;
        }
    }

    void FixedUpdate() {
        rigidBody.AddForce(moveDirection.normalized * MoveSpeed()/10,ForceMode.Impulse);
        rigidBody.linearVelocity = new Vector3(rigidBody.linearVelocity.x*(100-friction)/100,rigidBody.linearVelocity.y-gravity/10,rigidBody.linearVelocity.z*(100-friction)/100);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Implement this");
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Platform") {
            isGrounded = true;
        }
    }

    public float MoveSpeed() {
        if (sprintMode) {
            return sprintSpeed;
        } else {
            return walkSpeed;
        }
    }
}