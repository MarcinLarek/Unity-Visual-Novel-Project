using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicLayer
{
    public const string LAYER_OBJECT_NAME_FORMAT = "Layer: {0}";
    public int layerDepth = 0;
    public Transform panel;

    public GraphicObject currentGraphic { get; private set; } = null;

    public void SetTexture(string filePath, float transitionSpeed = 1f, Texture blendingTexture = null)
    {
        Texture tex = Resources.Load<Texture>(filePath);

        if(tex == null)
        {
            Debug.LogError($"Could not load graphic texture from path '{filePath}.' Please ensure it exists within Resources");
            return;
        }

        SetTexture(tex,transitionSpeed,blendingTexture, filePath);
    }

    public void SetTexture(Texture tex, float transitionSpeed = 1f, Texture blendingTexture = null,string filepath = "")
    {
        CreateGraphic(tex, transitionSpeed, filepath, blendingTexture: blendingTexture);

    }

    private void CreateGraphic<T>(T graphicData, float transitionSpeed, string filePath, bool useAudioForVideo = true, Texture blendingTexture = null)
    {
        GraphicObject newGraphic = null;

        if (graphicData is Texture)
            newGraphic = new GraphicObject(this, filePath, graphicData as Texture);

        currentGraphic = newGraphic;

        currentGraphic.FadeIn(transitionSpeed, blendingTexture);
    }
}
