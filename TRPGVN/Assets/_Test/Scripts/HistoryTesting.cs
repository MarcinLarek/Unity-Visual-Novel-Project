using History;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class HistoryTesting : MonoBehaviour
    {

        public DialogueData data;
        public List<AudioData> audioData;
        public List<GraphicData> graphicData;
        public List<CharacterData> characterData;



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            data = DialogueData.Capture();
            audioData = AudioData.Capture();
            graphicData = GraphicData.Capture();
            characterData = CharacterData.Capture();

        }
    }
}