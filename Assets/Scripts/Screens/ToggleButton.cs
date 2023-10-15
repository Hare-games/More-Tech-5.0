using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public Color OnState;
    public Color OffState;
    public bool IsActive = false;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { ChangeColor(); });
    }

    void ChangeColor()
    {
        IsActive = !IsActive;
        if (IsActive)
        {
            GetComponent<Image>().color = OnState;
        }
        else
        {
            GetComponent<Image>().color = OffState;
        }
    }
}
