using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YandexMaps;

public class YandexMap : MonoBehaviour, IDragHandler, IScrollHandler
{
    public static List<YandexMap> LoadedMap = new List<YandexMap>();
    public static List<Vector2> UsedVectors = new List<Vector2>() { Vector2.zero };
    public RawImage image;
    public GameObject Loadmap;

    public RectTransform rect;
    public Map.TypeMap typeMap;
    public Map.TypeMapLayer mapLayer;

    private Animator _animator;

    private Texture map_piece_texture;

    public GameObject LoadedObject;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Anim", true);
    }

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        LoadedMap.Add(this);

        Vector2 pos_p_x = GetComponent<RectTransform>().anchoredPosition + new Vector2(450, 0);
        Vector2 pos_m_x = GetComponent<RectTransform>().anchoredPosition + new Vector2(-450, 0);
        Vector2 pos_p_y = GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 450);
        Vector2 pos_m_y = GetComponent<RectTransform>().anchoredPosition + new Vector2(0, -450);
        List<bool> res = new List<bool>() { true, true, true, true };

        foreach (var item in UsedVectors)
        {
            if (item == pos_p_x) { res[0] = false; }
            if (item == pos_m_x) { res[1] = false; }
            if (item == pos_p_y) { res[2] = false; }
            if (item == pos_m_y) { res[3] = false; }
        }

        if (res[0] == true) UsedVectors.Add(pos_p_x);
        if (res[1] == true) UsedVectors.Add(pos_m_x);
        if (res[2] == true) UsedVectors.Add(pos_p_y);
        if (res[3] == true) UsedVectors.Add(pos_m_y);

        if (res[0] == true) { Instantiate(Loadmap, transform.parent).GetComponent<RectTransform>().anchoredPosition = pos_p_x; }
        if (res[1] == true) { Instantiate(Loadmap, transform.parent).GetComponent<RectTransform>().anchoredPosition = pos_m_x; }
        if (res[2] == true) { Instantiate(Loadmap, transform.parent).GetComponent<RectTransform>().anchoredPosition = pos_p_y; }
        if (res[3] == true) { Instantiate(Loadmap, transform.parent).GetComponent<RectTransform>().anchoredPosition = pos_m_y; }
    }

    public void PreLoadMap(float Latitude, float Longitude)
    {
        StartCoroutine(LoadMapValue(Latitude, Longitude));
    }

    public void LoadMapTexture()
    {
        image.texture = map_piece_texture;
    }

    public void OnDrag(PointerEventData data)
    {
        WorldMap.AddPos(data.delta * Time.deltaTime * 8);
    }

    public void OnScroll(PointerEventData eventData)
    {
        Debug.Log(eventData.scrollDelta);
        WorldMap.ScrollSIze(eventData.scrollDelta.y);
    }

    public void SetAnimOff()
    {
        _animator.SetBool("Anim", false);
        Destroy(LoadedObject);
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
        map_piece_texture = Map.GetTexture;
    }
}
