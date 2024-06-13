using UnityEngine;

public class OrientationLayoutAdjuster : MonoBehaviour
{
    public RectTransform portraitLayout; // Assign your portrait layout in the inspector
    public RectTransform landscapeLayout; // Assign your landscape layout in the inspector

    private ScreenOrientation lastOrientation;

    void Start()
    {
        lastOrientation = Screen.orientation;
        AdjustLayout(Screen.orientation);
    }

    void Update()
    {
        if (Screen.orientation != lastOrientation)
        {
            lastOrientation = Screen.orientation;
            AdjustLayout(lastOrientation);
        }
    }

    void AdjustLayout(ScreenOrientation orientation)
    {
        if (orientation == ScreenOrientation.Portrait || orientation == ScreenOrientation.PortraitUpsideDown)
        {
            ActivateLayout(portraitLayout);
            DeactivateLayout(landscapeLayout);
        }
        else if (orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight)
        {
            ActivateLayout(landscapeLayout);
            DeactivateLayout(portraitLayout);
        }
    }

    void ActivateLayout(RectTransform layout)
    {
        layout.gameObject.SetActive(true);
        foreach (RectTransform child in layout)
        {
            child.gameObject.SetActive(true);
        }
    }

    void DeactivateLayout(RectTransform layout)
    {
        layout.gameObject.SetActive(false);
        foreach (RectTransform child in layout)
        {
            child.gameObject.SetActive(false);
        }
    }
}
