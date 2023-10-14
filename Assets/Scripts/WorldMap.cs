using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YandexMaps;

public class WorldMap : MonoBehaviour
{
    public static WorldMap Instance;

    public int Size;

    public float StartLatitude; //Широта
    public float StartLongitude; //Долгота

    public int CellX;
    public int CellY;

    public int StartX;
    public int StartY;

    public float width_multiply;
    public float height_miltiply;

    public RectTransform canvas;
    public GameObject YandexMap;
    private GameObject[,] YandexWorld;

    private float _world_size_square = 90;

    private void Awake()
    {
        Map.Height = 450;
        Map.Width = 450;
        for (int i = 0; i < Size - 1; i++)
        {
            _world_size_square = _world_size_square / 2;
        }
        Instance = this;
    }

    private void Start()
    {
        YandexWorld = new GameObject[CellX, CellY];
        for (int i = 0; i < CellX; i++)
        {
            for (int k = 0; k < CellY; k++)
            {
                var obj = Instantiate(YandexMap, transform.parent);
                obj.GetComponent<RectTransform>().anchoredPosition += new Vector2(i * 900, k * 900);
                YandexWorld[i, k] = obj;
            }
        }

        GetComponent<RectTransform>().anchoredPosition -= new Vector2(450 * (CellX-1), 450 * (CellY-1));// new Vector2(450/2, 450/2);
        StartLatitude = StartLatitude - CellX * _world_size_square;
        StartLongitude = StartLongitude - CellY * _world_size_square;

        StartCoroutine(LoadMap());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F");
            StartCoroutine(LoadMap());
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

    IEnumerator LoadMap()
    {
        float LoadingProcess = 0;
        float LoadingSteps = CellX * CellY;

        for (int i = 0; i < CellX; i++)
        {
            for (int k = 0; k < CellY; k++)
            {
                YandexWorld[i, k].GetComponent<YandexMap>().PreLoadMap(StartLatitude + k * _world_size_square * width_multiply, StartLongitude + i * _world_size_square * height_miltiply);
                yield return new WaitForSeconds(1.5f);

                LoadingProcess = i * CellY + k;
                Debug.Log($"Loading: {LoadingProcess / LoadingSteps}");
            }
        }

        for (int i = 0; i < CellX; i++)
        {
            for (int k = 0; k < CellY; k++)
            {
                YandexWorld[i, k].GetComponent<YandexMap>().LoadMapTexture();
            }
        }

    }
}
