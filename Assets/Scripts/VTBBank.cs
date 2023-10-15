using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VTBBank : MonoBehaviour
{
    public static VTBBank DrawedPass;
    public GameObject path;

    public static int mult = 10;
    public float StartLatitude; //??????
    public float StartLongitude; //???????

    private RectTransform rect;

    private void Start()
    {
        float step_latitude = StartLatitude - WorldMap.Instance.StartLongitude;
        float step_longitude = StartLongitude - WorldMap.Instance.StartLatitude;

        WorldMap.GetSize();
        rect = GetComponent<RectTransform>();

        Debug.Log($"{step_longitude}:{WorldMap.Instance.width_multiply}:{450}");
        rect.anchoredPosition = new Vector2(((step_longitude * WorldMap.Instance.width_multiply) / WorldMap.Instance._world_size_square) * 450, ((step_latitude * WorldMap.Instance.height_miltiply) / WorldMap.Instance._world_size_square) * 450)/2;
    }

    private void DrawPass()
    {
        path.SetActive(true);
    }

    private void RemovePass()
    {
        path.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (DrawedPass != null)
        {
            DrawedPass.RemovePass();
        }
        DrawedPass = this;
        DrawedPass.DrawPass();
    }
}
