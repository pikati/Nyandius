using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTextView : MonoBehaviour
{
    [SerializeField]
    private GameObject[] texts;
    
    public void DispText(int index, bool b)
    {
        texts[index].SetActive(b);
    }
}
