using System.Collections;
using UnityEngine;

public class HandleAnimation : MonoBehaviour
{
    public Animator animator;
    public bool isAnimating = false;


    private void Update()
    {
        if(isAnimating)
        {
            animator.enabled = true;
        }
    }

    public void HandleAnim(bool play)
    {
        StartCoroutine(HandleAnimCoroutine(play));
    }
    
    IEnumerator HandleAnimCoroutine(bool play)
    {
        if (play)
        {
            isAnimating = true;
            animator.enabled = true;

            yield return null;

            animator.SetBool("Play", true);
        }
        else
        {
            isAnimating = false;
            animator.SetBool("Play", false);

            yield return new WaitForSeconds(0.3f);
            animator.enabled = false;
        }
    }
}
