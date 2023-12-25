using UnityEngine;

namespace Player.Movement
{
    public class Mover : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float rotationSpeed = 10f;
        public float jumpForce = 10f;

        private Animator animator;
        private Rigidbody rb;
        private bool isGrounded;

        private void Start()
        {
            // Get the required components
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // Handle player movement
            HandleMovement();

            // Handle player jumping
            HandleJump();

            // Update animator parameters for character animations
            UpdateAnimator();
        }

        private void HandleMovement()
        {
            float horizontal = -Input.GetAxis("Horizontal");
            float vertical = -Input.GetAxis("Vertical");

            // Use the W, A, S, D keys for movement
            Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);

            // Rotate the player in the direction of movement
            if (movement != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(horizontal, 0f, vertical), Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * -Time.deltaTime);
            }
        }

        private void HandleJump()
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        private void UpdateAnimator()
        {
            // Use the default "Speed" parameter if it exists in your Animator
            if (animator)
            {
                float moveMagnitude = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude;
                animator.SetFloat("forwardspeed", moveMagnitude);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }
    }
}
