using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using VISUALNOVEL;
using static UnityEngine.Rendering.HableCurve;

namespace TESTING
{
    public class TestDialogueFiles : MonoBehaviour
    {
        [SerializeField] private TextAsset fileToRead = null;

        // Start is called before the first frame update
        void Start()
        {
            StartConversation();

        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.DownArrow))
            //    DialogueSystem.instance.dialogueContainer.Hide();
            //
            //if (Input.GetKeyDown(KeyCode.UpArrow))
            //   DialogueSystem.instance.dialogueContainer.Show();
        }

        void StartConversation()
        {
            string fullPath = AssetDatabase.GetAssetPath(fileToRead);

            int resourcesIndex = fullPath.IndexOf("Resources/");
            string relativePath = fullPath.Substring(resourcesIndex + 10);

            string filePath = Path.ChangeExtension(relativePath, null);

            VNManager.instance.LoadFile(filePath);
        }
    }
}