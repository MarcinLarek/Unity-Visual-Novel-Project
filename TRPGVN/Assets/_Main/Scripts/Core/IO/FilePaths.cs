using UnityEngine;

public class FilePaths
{
    public static readonly string root = $"{Application.dataPath}/gameData/";

    //ResourcesPaths
    public static readonly string resources_graphics = "Graphics/";
    public static readonly string resources_backgroundImages = $"{resources_graphics}BG_Images/";
    public static readonly string resources_backgroundVideos = $"{resources_graphics}BG_Videos/";
    public static readonly string resources_blendTextures = $"{resources_graphics}TransitionEffects/";

}
