using System.Collections;
using TMPro;
using UnityEngine;

namespace Ac.At.FhStp.UnityUDPDemo
{

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FadeText : MonoBehaviour
    {

        [SerializeField] private float visibleTime;
        [SerializeField] private float fadeTime;

        private TextMeshProUGUI textComponent;


        private string DisplayedText
        {
            set => textComponent.text = value;
        }

        private Color TextColor
        {
            get => textComponent.color;
            set => textComponent.color = value;
        }

        private float TextAlpha
        {
            get => TextColor.a;
            set => TextColor = TextColor.WithA(value);
        }


        protected virtual void Awake() => 
            textComponent = GetComponent<TextMeshProUGUI>();

        public void Display(string s)
        {
            IEnumerator FadeOut()
            {
                yield return new WaitForSeconds(visibleTime);

                var t = 0f;
                while (t < 1)
                {
                    t = Mathf.MoveTowards(t, 1, Time.deltaTime / fadeTime);
                    TextAlpha = 1 - t;
                    yield return null;
                }
            }

            StopAllCoroutines();
            DisplayedText = s;
            TextAlpha = 1;
            StartCoroutine(FadeOut());
        }

        protected void Clear() =>
            DisplayedText = string.Empty;

    }

}