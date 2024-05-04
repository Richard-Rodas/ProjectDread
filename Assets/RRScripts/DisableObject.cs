using UnityEngine;
using System.Collections;
namespace TMPro.Examples
{
    public class DisableObject : MonoBehaviour
    {
        [SerializeField] private GameObject myObj;
        private TMP_Text m_TextComponent;
        public float FadeSpeed = 1.0F;
        [SerializeField] private float activeTime = 5.0f;
        public int RolloverCharacterSpread = 10;
        public Color ColorTint;
        private bool isFading = false; // Flag to track if fading is in progress

        void Awake()
        {
            m_TextComponent = GetComponent<TMP_Text>();
        }
        void Start()
        {
            StartCoroutine(AnimateVertexColors());
        }

        IEnumerator AnimateVertexColors()
        {
            // Check if the fading process is already running
            if (isFading)
                yield break;

            isFading = true; // Set flag to indicate that fading has started

            yield return new WaitForSeconds(activeTime);

            // Need to force the text object to be generated so we have valid data to work with right from the start.
            m_TextComponent.ForceMeshUpdate();
            TMP_TextInfo textInfo = m_TextComponent.textInfo;
            Color32[] newVertexColors;
            int currentCharacter = 0;
            int startingCharacterRange = currentCharacter;
            bool isRangeMax = false;

            while (!isRangeMax)
            {
                int characterCount = textInfo.characterCount;
                byte fadeSteps = (byte)Mathf.Max(1, 255 / RolloverCharacterSpread);

                for (int i = startingCharacterRange; i < currentCharacter + 1; i++)
                {
                    if (!textInfo.characterInfo[i].isVisible) continue;

                    int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                    newVertexColors = textInfo.meshInfo[materialIndex].colors32;
                    int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                    byte alpha = (byte)Mathf.Clamp(newVertexColors[vertexIndex + 0].a - fadeSteps, 0, 255);

                    newVertexColors[vertexIndex + 0].a = alpha;
                    newVertexColors[vertexIndex + 1].a = alpha;
                    newVertexColors[vertexIndex + 2].a = alpha;
                    newVertexColors[vertexIndex + 3].a = alpha;

                    newVertexColors[vertexIndex + 0] = (Color)newVertexColors[vertexIndex + 0] * ColorTint;
                    newVertexColors[vertexIndex + 1] = (Color)newVertexColors[vertexIndex + 1] * ColorTint;
                    newVertexColors[vertexIndex + 2] = (Color)newVertexColors[vertexIndex + 2] * ColorTint;
                    newVertexColors[vertexIndex + 3] = (Color)newVertexColors[vertexIndex + 3] * ColorTint;

                    if (alpha == 0)
                    {
                        startingCharacterRange += 1;
                        if (startingCharacterRange == characterCount)
                        {
                            m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                            // Delay to ensure the fade-out effect is completed
                            yield return new WaitForSeconds(FadeSpeed);
                            // Disable the object after the fade-out effect is completed
                            myObj.SetActive(false);
                            // Set flag to indicate that fading has completed
                            isFading = false;
                            // Exit the coroutine
                            yield break;
                        }
                    }
                }
                m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                if (currentCharacter + 1 < characterCount) currentCharacter += 1;
                yield return new WaitForSeconds(0.25f - FadeSpeed * 0.01f);
            }
        }
    }

   
}
