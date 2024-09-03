using UnityEngine;
using UnityEngine.InputSystem;

public class TestAnimatorTest : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
    }

    private bool check = false;
    private bool triggered = false;
    void Update()
    {
        if (check)
        {
            check = false;
            // Debug.Log("Checked, the next frame of state2 trigger is state3: " 
            //           + animator.IsInTransition(0));
            Debug.Log("Checked, the next frame of state2 trigger is state3: " 
                      + animator.GetCurrentAnimatorStateInfo(0).IsName("State3"));
        }
        if (!triggered && animator.GetCurrentAnimatorStateInfo(0).IsName("State2"))
        {
            if (Mouse.current.leftButton.isPressed)
            {
                animator.SetTrigger("2to3");
                Debug.Log("Triggered");
                check = true;
                triggered = true;
            }
        }
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }
}