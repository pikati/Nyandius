using System.Collections;
using TMPro;
using UnityEngine;

public class LifeView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _life;
    
    public void UpdateText(int n)
    {
        _life.text = n.ToString();
    }
}
