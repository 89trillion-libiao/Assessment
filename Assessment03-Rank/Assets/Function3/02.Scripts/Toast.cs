using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    [SerializeField] private Text messageText;

    
    public void Show(string message)
    {
        messageText.text = message;
        SetActive(true);
    }


    public void SetActive(bool isActive)
    {
        
        this.gameObject.SetActive(isActive);
        
    }
}
