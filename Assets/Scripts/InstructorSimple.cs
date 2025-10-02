using UnityEngine;

public class InstructorSimple : MonoBehaviour
{
    public Animator animator;

    public void StartTalking()
    {
        if (animator != null)
            animator.SetTrigger("Talk");
        Debug.Log("بدأ الكلام");
    }

    public void StopTalking()
    {
        if (animator != null)
            animator.SetTrigger("Idle");
        Debug.Log("توقف الكلام");
    }
}