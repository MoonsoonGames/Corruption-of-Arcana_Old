using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionAnim : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        DontDestroyOnLoad(this);

        animator = GetComponentInChildren<Animator>();
    }

    public void PlayAnim()
    {
        StartCoroutine(ITransitionAnim(1.5f));
    }

    IEnumerator ITransitionAnim(float delay)
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("Open");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayAnim();
        }
    }
}
