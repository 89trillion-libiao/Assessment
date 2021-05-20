using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    [SerializeField] private Animator characterAnimator;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            characterAnimator.SetTrigger("Run");
        }else if (Input.GetKeyDown(KeyCode.I))
        {
            characterAnimator.SetTrigger("Idle");
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            characterAnimator.SetTrigger("Attack");
        }
    }

    
    
}
