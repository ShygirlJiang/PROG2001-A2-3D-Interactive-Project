using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{
    public float speed = 50f; // UI 坐标系的单位通常比世界坐标大，建议调高数值

    private Vector2 startAnchoredPosition; // 记录 UI 的锚点位置
    private RectTransform rectTransform;

    void Awake()
    {
        // 建议在 Awake 中获取组件，确保 OnEnable 执行时它已经存在
        rectTransform = GetComponent<RectTransform>();

        // 记录在 Inspector 面板里看到的那个 Pos X 和 Pos Y
        startAnchoredPosition = rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        // UI 重置位置的最佳方式
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = startAnchoredPosition;
        }
    }

    void Update()
    {
        if (rectTransform.localPosition.y > 1000)
        {
            return;
        }
            // 使用 Vector2.up 向上移动
            // UI 元素不建议使用 Translate，直接累加 anchoredPosition 性能更好且坐标精确
            rectTransform.anchoredPosition += Vector2.up * speed * Time.deltaTime;
    }
}