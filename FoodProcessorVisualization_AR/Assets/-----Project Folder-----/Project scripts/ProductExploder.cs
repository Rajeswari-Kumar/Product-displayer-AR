using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductExploder : MonoBehaviour
{
   

    private bool exploded = false;
    public bool alreadyExploded = false;

    public Animator animator;

    public bool isExploding = false;
    void Start()
    {

    }
    private void OnEnable()
    {

    }

    private void Update()
    {
        if(isExploding)
        {
            animator.enabled = true;
        }
    }

    public void ToggleExplode(bool exploded)
    {
        if (!exploded)
            StartCoroutine(RestoreParts());
        else if(exploded)
            StartCoroutine(ExplodeParts());

    }

    IEnumerator ExplodeParts()
    {
        if (!alreadyExploded)
        {
            isExploding = true;
            animator.enabled = true;
            alreadyExploded = true;
            yield return new WaitForSeconds(0.3f);
            animator.SetBool("Explode", true);
        }
    }

    IEnumerator RestoreParts()
    {
        if(alreadyExploded)
        {
            alreadyExploded = false;
            animator.SetBool("Explode", false);
            yield return new WaitForSeconds(0.5f);
            animator.enabled = false;
            isExploding = false;
           
        }
    }
}
