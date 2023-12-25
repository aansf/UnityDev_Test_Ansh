using UnityEngine;

namespace Player.Movement
{
    public class GravityController : MonoBehaviour
    {
        public float gravity = 9.8f;
        public float flipDuration = 1.0f;
        public GameObject hologramPrefab;
        public float groundCheckDistance = 0.1f; // Adjust this value based on your character's size

        private bool isFlipping = false;
        private float flipTimer = 0.0f;
        private Vector3 currentGravityDirection = Vector3.down;
        private GameObject hologram;
        private bool isGrounded;

        private void Start()
        {
            // Instantiate the hologram at the initial position
            CreateHologram();
        }

        private void Update()
        {
            // Check arrow key inputs for rotating the hologram
            HandleHologramRotation();

            // Check Enter key input for flipping gravity
            HandleFlipGravity();

            // Apply gravity
            ApplyGravity();

            // Check if the player is grounded
            CheckGrounded();

            // Check for game over condition
            CheckGameOver();
        }

        private void HandleHologramRotation()
        {
            if (hologram != null)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    RotateHologram(Vector3.left);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    RotateHologram(Vector3.right);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    RotateHologram(Vector3.down);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    RotateHologram(Vector3.up);
                }
            }
        }

        private void HandleFlipGravity()
        {
            if (Input.GetKeyDown(KeyCode.Return) && !isFlipping)
            {
                StartFlip();
            }

            if (isFlipping)
            {
                // Lerp the rotation smoothly during the flip duration
                flipTimer += Time.deltaTime;
                float t = Mathf.Clamp01(flipTimer / flipDuration);

                // Rotate around the base (feet) of the player
                Vector3 pivotPoint = transform.position - Vector3.up * 0.5f; // Adjust the value based on your character's height
                Quaternion toRotation = Quaternion.FromToRotation(transform.up, currentGravityDirection) * Quaternion.Euler(pivotPoint);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, t);

                if (t >= 1.0f)
                {
                    isFlipping = false;
                }
            }
        }

        private void ApplyGravity()
        {
            // Apply gravity force
            Vector3 gravityForce = currentGravityDirection * gravity;
            GetComponent<Rigidbody>().AddForce(gravityForce, ForceMode.Acceleration);
        }

        private void StartFlip()
        {
            isFlipping = true;
            flipTimer = 0.0f;

            // Rotate the player to the specified gravity direction
            currentGravityDirection = hologram ? hologram.transform.forward : Vector3.down;
        }

        private void CreateHologram()
        {
            // Instantiate the hologram prefab at the player's position
            hologram = Instantiate(hologramPrefab, transform.position, Quaternion.identity);
        }

        private void RotateHologram(Vector3 rotationDirection)
        {
            // Rotate the hologram in the specified direction
            hologram.transform.rotation = Quaternion.LookRotation(rotationDirection);
        }

        private void CheckGrounded()
        {
            // Perform a raycast downward to check if the player is grounded
            isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        }

        private void CheckGameOver()
        {
            // Check for game over condition (player is not grounded)
            if (!isGrounded)
            {
                // Implement your game over logic here
                Debug.Log("Game Over - Player fell!");
            }
        }
    }
}
