using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnScreenMap : MonoBehaviour
{
    public UnityEvent event_sda;

    private void Update()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        float width = Screen.width;
        float height = Screen.height;
        Debug.Log(pos.x < width && pos.y < height && pos.x > 0 && pos.y > 0);

        event_sda.Invoke();
    }
}
