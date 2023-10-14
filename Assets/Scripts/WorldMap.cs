using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YandexMaps;

public class WorldMap : MonoBehaviour
{
    public static List<YandexMap> UpdateList = new List<YandexMap>();
    bool continue_update = true;

    public static WorldMap Instance;

    public int Size;

    public float StartLatitude; //??????
    public float StartLongitude; //???????

    public float width_multiply;
    public float height_miltiply;

    float[] width_arr = new float[]
    {
        0.8f,
        0.8f,
        0.8f,
        0.83f,
        0.95f,
        1.025f,
        1.055f,
        1.075f,
        1.095f,
        1.095f,
        1.095f,
        1.095f,
        1.095f,
        1.095f,
        1.095f,
        1.095f,
        1.095f,
        1.095f,
        1.095f,
        1.095f,
        1.095f
    };

    public RectTransform canvas;

    public float _world_size_square;

    private void Awake()
    {
        Instance = this;
        Map.Height = 450;
        Map.Width = 450;
        Map.Size = GetSize();
    }

    private void Update()
    {
        if (continue_update)
        {
            if (UpdateList.Count > 0)
            {
                continue_update = false;
                StartCoroutine(UpdateState(UpdateList[0]));
                UpdateList.RemoveAt(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            UpdateList.AddRange(YandexMap.LoadedMap);
        }
    }
    public static void AddPos(Vector2 pos)
    {
        Instance.transform.Translate(pos, Space.Self);
    }

    public static void ScrollSIze(float value)
    {
        Instance.transform.localScale += Vector3.one * value;
    }

    public static int GetSize()
    {
        return Instance.Size;
    }

    public static void SetSize(int size)
    {
        Instance.width_multiply = Instance.width_arr[size];
        Instance.Size = size;
        float world_size_sq = 90f;

        for (int i = 0; i < Instance.Size - 2; i++)
        {
            world_size_sq = world_size_sq / 2;
        }

        Instance._world_size_square = world_size_sq;
    }

    public static Vector2 GetWorldPos(Vector2 unity_pos)
    {
        float mult_x = unity_pos.x / 450f;
        float mult_y = unity_pos.y / 450f;

        SetSize(Instance.Size);
        return new Vector2(Instance.StartLongitude + mult_x * Instance._world_size_square * Instance.width_multiply, Instance.StartLatitude + mult_y * Instance._world_size_square * Instance.height_miltiply);
    }

    IEnumerator UpdateState(YandexMap map)
    {
        Vector2 world_pos = GetWorldPos(new Vector2(map.rect.anchoredPosition.y, map.rect.anchoredPosition.x));
        map.PreLoadMap(world_pos.x, world_pos.y, GetSize());
        yield return new WaitForSeconds(1f);
        continue_update = true;
    }
}
