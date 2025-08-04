using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private float lastKeyTime = 0f;
    private KeyCode lastKey = KeyCode.None;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Walk input
        bool walkInput = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        animator.SetBool("IsWalking", walkInput);

        // Run input (double-tap A or D)
        if (DetectDoubleTap(KeyCode.A) || DetectDoubleTap(KeyCode.D))
        {
            animator.SetBool("IsRunning", true);
        }
        else if (!walkInput)
        {
            animator.SetBool("IsRunning", false);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger("Jump");

        // Punches
        if (Input.GetMouseButtonDown(0))  // Left click
            animator.SetTrigger("LeftPunch");

        if (Input.GetMouseButtonDown(1))  // Right click
            animator.SetTrigger("RightPunch");

        // Kick
        if (Input.GetKeyDown(KeyCode.C)) {
            animator.SetTrigger("Kick");
            Debug.Log("Update kick");
        }
            
    }

    private bool DetectDoubleTap(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            float time = Time.time;
            if (lastKey == key && time - lastKeyTime < 0.3f)
            {
                lastKeyTime = 0f;
                return true;
            }
            lastKey = key;
            lastKeyTime = time;
        }
        return false;
    }
}
