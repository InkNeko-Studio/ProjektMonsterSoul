using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Shared.Enemy
{
    public class HitCanvas : MonoBehaviour
    {
        [SerializeField] private float colorSpeed;
        [SerializeField] private float animationSpeed;
        [SerializeField] private List<TMP_Text> hitElements;

        private Canvas _canvas;
        private static HitCanvas _instance;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _instance = this;
        }

        public static void SetHit(Vector2 position, int value)
        {
            _instance.SetHitElement(position, value);
        }

        private void SetHitElement(Vector2 position, int value)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.GetComponent<RectTransform>(),
                screenPosition,
                _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera,
                out canvasPosition
            );
            if (hitElements.Count > 0)
            {
                var color = hitElements[0].color;
                color.a = 1f;
                hitElements[0].color = color;
                hitElements[0].rectTransform.anchoredPosition = canvasPosition;
                hitElements[0].text = value.ToString();
                StartCoroutine(HitAnimation());
            }
        }

        private IEnumerator HitAnimation()
        {
            var element = hitElements[0];
            hitElements.RemoveAt(0);

            while (element.color.a > 0f)
            {
                element.color -= new Color(0, 0, 0, Time.deltaTime * colorSpeed);
                element.rectTransform.anchoredPosition += new Vector2(0f, animationSpeed * Time.deltaTime);
                yield return null;
            }
            
            hitElements.Add(element);
        }
    }
}
