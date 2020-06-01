using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    public void Init(Animator animator)
    {
        this.animator = animator;
    }

    public void Reset()
    {
        animator.SetBool("DistanceAttack", false);
    }

    public void CanMove()
    {
        animator.SetBool("CanMove", true);
    }

    private void CantMove()
    {
        animator.SetBool("CanMove", false);
    }

    public void Movement(Vector2 movement)
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    public bool CantAction()
    {
        return animator.GetBool("CanMove") == true;
    }

    public void DistanceAttack()
    {
        CantMove();
        animator.SetBool("DistanceAttack", true);
    }
}
