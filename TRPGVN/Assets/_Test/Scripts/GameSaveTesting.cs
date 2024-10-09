using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VISUALNOVEL;

public class GameSaveTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VNGameSave.activeFile = new VNGameSave();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            VNGameSave.activeFile.Save();
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            VNGameSave.activeFile = FileManager.Load<VNGameSave>($"{FilePaths.gameSaves}1{VNGameSave.FILE_TYPE}");
            VNGameSave.activeFile.Load();
        }
    }
}
