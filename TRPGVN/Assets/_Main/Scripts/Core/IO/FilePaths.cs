using UnityEngine;

public class FilePaths
{
    private const string HOMEDIRECTORY_SYMBOl = "~/";

    public static readonly string root = $"{Application.dataPath}/gameData/";

    //Runtime Paths
    public static readonly string gameSaves = $"{runtimePath}Save Files/";

    //ResourcesPaths
    public static readonly string resources_fonts = "Fonts/";

    public static readonly string resources_graphics = "Graphics/";
    public static readonly string resources_backgroundImages = $"{resources_graphics}BG_Images/";
    public static readonly string resources_backgroundVideos = $"{resources_graphics}BG_Videos/";
    public static readonly string resources_blendTextures = $"{resources_graphics}TransitionEffects/";

    public static readonly string resources_audio = "Audio/";
    public static readonly string resources_sfx = $"{resources_audio}SFX/";
    public static readonly string resources_voices = $"{resources_audio}Voices/";
    public static readonly string resources_music = $"{resources_audio}Music/";
    public static readonly string resources_ambience = $"{resources_audio}Ambience/";

    public static readonly string resources_dialogueFiles = $"DialogueFiles/";

    public static string GetPathToResource(string defaultPath, string resourceName)
    {
        if(resourceName.StartsWith(HOMEDIRECTORY_SYMBOl))
            return resourceName.Substring(HOMEDIRECTORY_SYMBOl.Length);

        return defaultPath + resourceName;

    }

    public static string runtimePath
    {
        get
        {
            #if UNITY_EDITOR
                        return "Assets/appdata";
            #else
                        return Application.persistentDataPath + "/appdata/";
            #endif
        }
    }

}
