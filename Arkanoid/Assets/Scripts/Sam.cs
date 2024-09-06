using UnityEngine;

public class Sam : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    private void Start()
    {
        playerController = PlayerController.Instance;
        playerController.onMousePressed += Hide;

        animator = GetComponent<Animator>();
        animator.SetBool("IsHided", false);
    }
    private void Hide(float angle, float force)
    {
        animator.SetBool("IsHided", true);
    }
}
