using UnityEngine;

public class ObjectHighlight : MonoBehaviour
{
    private Renderer[] renderers;
    private Color originalColor;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0)
            originalColor = renderers[0].material.color;
    }

    public void HighlightOn()
    {
        foreach (Renderer r in renderers)
        {
            r.material.color = Color.yellow;
            r.material.EnableKeyword("_EMISSION");
            r.material.SetColor("_EmissionColor", Color.yellow * 2f);
        }
        Debug.Log("✨ تم الإضاءة: " + gameObject.name);
    }

    public void HighlightOff()
    {
        foreach (Renderer r in renderers)
        {
            r.material.color = originalColor;
            r.material.DisableKeyword("_EMISSION");
        }
        Debug.Log("💡 إلغاء الإضاءة: " + gameObject.name);
    }
}