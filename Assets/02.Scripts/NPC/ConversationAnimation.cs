using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationAnimation : MonoBehaviour
{
   private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetConversation()
    {
        animator.SetBool("conversation",true);
    }

        public void DestroyConversation()
    {
        animator.SetBool("conversation",false);
    }

}
