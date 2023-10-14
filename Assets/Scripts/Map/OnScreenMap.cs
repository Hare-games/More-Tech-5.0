using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnScreenMap : MonoBehaviour
{
    public UnityEvent event_sda;

    private void Update()
    {
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        float width = Screen.width;
        float height = Screen.height;
        bool result = pos.x > 0 && pos.x < 1 && pos.y < 1 && pos.y > 0;
        if (!result) return;

        event_sda.Invoke();
    }
}
