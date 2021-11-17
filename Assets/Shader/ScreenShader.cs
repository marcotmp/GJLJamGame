using UnityEngine;

public class ScreenShader : MonoBehaviour
{
    public Material m_renderMaterial;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, m_renderMaterial);
    }
}