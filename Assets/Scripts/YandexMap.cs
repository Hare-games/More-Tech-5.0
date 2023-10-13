using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using YandexMaps;

public class YandexMap : MonoBehaviour
{
    public RawImage image;
    public float Latitude;
    public float Longitude;
    public int Size;

    public Map.TypeMap typeMap;
    public Map.TypeMapLayer mapLayer;

    private void Start()
    {
        LoadMap();
    }

    public void LoadMap()
    {
        Map.EnabledLayer = true;
        Map.Size = 4;
        Map.SetTypeMap = typeMap;
        Map.SetTypeMapLayer = mapLayer;
        Map.LoadMap();
        StartCoroutine(LoadTexture());
    }

    IEnumerator LoadTexture()
    {
        while (true)
        {
            Map.LoadMap();

            Map.Latitude = Latitude;
            Map.Longitude = Longitude;
            Map.Size = Size;

            yield return new WaitForSeconds(0.05F);
            image.texture = Map.GetTexture;
        }
    }
}

[CustomEditor(typeof(YandexMap))]
public class YandexMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var map = (YandexMap)target;
        if (map.image == null) return;

        map.image = (RawImage)EditorGUILayout.ObjectField("Изображение:", map.image, typeof(RawImage));
        map.Latitude = EditorGUILayout.FloatField("Широта:", map.Latitude);
        map.Longitude = EditorGUILayout.FloatField("Долгота:", map.Longitude);
        map.Size = EditorGUILayout.IntField("Размер:", map.Size);
    }
}
