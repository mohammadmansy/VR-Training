using UnityEngine;

public class InstructorSimple : MonoBehaviour
{
    public Animator animator;

    public void StartTalking()
    {
        if (animator != null)
            animator.SetTrigger("Talk");
    }
    public void Point()
    {
        if (animator != null)
            animator.SetTrigger("Point");
    }

    public void StopTalking()
    {
        if (animator != null)
            animator.SetTrigger("Idle");
    }
    public void StopPointing()
    {
        if (animator != null)
            animator.SetTrigger("Idle");
    }
}