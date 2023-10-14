using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnScreenMap : MonoBehaviour
{
    public LoadedMap Loaded;
    private bool is_used = true;
    private bool is_inited = false;

    private void Update()
    {
        if (!is_used) return;
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        bool result = pos.x > 0 && pos.x < 1 && pos.y < 1 && pos.y > 0;
        if (!result) return;

        is_inited = Loaded.LoadYandexMap();

        if (!is_inited) return;

        is_used = false;
    }
}
