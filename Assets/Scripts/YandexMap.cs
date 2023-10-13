using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YandexMaps;

public class YandexMap : MonoBehaviour, IDragHandler, IScrollHandler
{
    public RawImage image;

    public Map.TypeMap typeMap;
    public Map.TypeMapLayer mapLayer;

    public void LoadMap(float Latitude, float Longitude)
    {
        StartCoroutine(LoadMapValue(Latitude, Longitude));
    }

    public void OnDrag(PointerEventData data)
    {
       WorldMap.AddPos(data.delta * Time.deltaTime * 8);
    }

    public void OnScroll(PointerEventData eventData)
    {
        WorldMap.ScrollSIze(eventData.scrollDelta.y * 0.5f);
    }

    IEnumerator LoadMapValue(float Latitude, float Longitude)
    {
        Map.EnabledLayer = true;
        Map.Size = WorldMap.GetSize();
        Map.SetTypeMap = typeMap;
        Map.SetTypeMapLayer = mapLayer;
        Map.Latitude = Latitude;
        Map.Longitude = Longitude;
        Map.LoadMap();
        yield return new WaitForSeconds(1.4f);
        image.texture = Map.GetTexture;
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
    }
}
