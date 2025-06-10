using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonModeColorManager : MonoBehaviour
{
    public Image targetImage;
    public Button musicModeButton;
    public Button moveModeButton;
    public Button editModeButton;
    public Button loopModeButton;
    public Button linkModeButton;
    
    public Color musicModeColor = Color.green;
    public Color moveModeColor = Color.blue;
    public Color loopModeColor = Color.yellow;
    public Color linkModeColor = Color.magenta;
    public Color editModeColor = Color.HSVToRGB(30f / 360f, 1f, 1f);
    public float duration = 0.4f;

    private void Start()
    {
        if (musicModeButton != null)
        {
            musicModeButton.onClick.AddListener(() => StartCoroutine(ChangeColorOverTime(targetImage, musicModeColor, duration)));
        }
        if (moveModeButton != null)
        {
            moveModeButton.onClick.AddListener(() => StartCoroutine(ChangeColorOverTime(targetImage, moveModeColor, duration)));
        }
        if (editModeButton != null)
        {
            editModeButton.onClick.AddListener(() => StartCoroutine(ChangeColorOverTime(targetImage, editModeColor, duration)));
        }
        if (loopModeButton != null)
        {
            loopModeButton.onClick.AddListener(() => StartCoroutine(ChangeColorOverTime(targetImage, loopModeColor, duration)));
        }
        if (linkModeButton != null)
        {
            linkModeButton.onClick.AddListener(() => StartCoroutine(ChangeColorOverTime(targetImage, linkModeColor, duration)));
        }
    }

    private IEnumerator ChangeColorOverTime(Image image, Color endColor, float duration)
    {
        if (image == null)
        {
            yield break;
        }

        Color startColor = image.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            image.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.color = endColor;
    }
}
