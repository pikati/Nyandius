using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpView : MonoBehaviour
{
    [SerializeField]
    private Image[] _powerUpImages;
    private Color _selectColor = Color.yellow;
    private Color _defaultColor = Color.white;

    public void SetSelectColor(int index)
    {
        if (index == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                _powerUpImages[i].color = _defaultColor;

            }
            return;
        }

        try
        {
            for (int i = 0; i < 6; i++)
            {
                if (index - 1 == i)
                {
                    _powerUpImages[i].color = _selectColor;
                }
                else
                {
                    _powerUpImages[i].color = _defaultColor;
                }
            }
        }
        catch (System.NullReferenceException)
        {
            Debug.Log($"index: {index - 1} is null occurred in PowerUpView SetSelectColor");
        }
        catch (System.Exception e)
        {
            Debug.Log($" {e.Message} occurred in PowerUpView SetSelectColor");
        }
    }

}
