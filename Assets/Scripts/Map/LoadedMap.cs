using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadedMap : MonoBehaviour
{
    [Header("Generator")]
    public GameObject YandexMap;

    private Animator _anim;
    private RectTransform _rect;
    private bool Inited = false;

    public void Start()
    {
        _rect = GetComponent<RectTransform>();
        _anim = GetComponent<Animator>();
        _anim.SetBool("Anim", true);
        StartCoroutine(PlayAnim());
    }

    public void StopAnimation()
    {
        _anim.SetBool("Anim", false);
    }

    public void LoadYandexMap()
    {
        if (!Inited) return;

        var obj = Instantiate(YandexMap, transform.parent);
        obj.GetComponent<RectTransform>().anchoredPosition = _rect.anchoredPosition;
        Destroy(gameObject);
    }

    IEnumerator PlayAnim()
    {
        _anim.Play("SetOn");
        yield return new WaitForSeconds(0.02f);
        Inited = true;
    }
}
