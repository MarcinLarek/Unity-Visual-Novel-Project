using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class Testing_architect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        string[] lines = new string[5]
        {
            "First Testing Line, First Testing Line, First Testing Line",
            "Second Testing Line, Second Testing Line, Second Testing Line",
            "Third Testing Line, Third Testing Line, Third Testing Line",
            "Fourth Testing Line, Fourth Testing Line, Fourth Testing Line",
            "Fifth Testing Line, Fifth Testing Line, Fifth Testing Line, Fifth Testing Line, Fifth Testing Line, Fifth Testing Line, Fifth Testing Line, Fifth Testing Line, Fifth Testing Line, Fifth Testing Line, Fifth Testing Line, Fifth Testing Line, Fifth Testing Line, Fifth Testing Line, Fifth Testing Line",
        };

        // Start is called before the first frame update
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.fade;
        }

        // Update is called once per frame
        void Update()
        {
            if(bm != architect.buildMethod)
            {
                architect.buildMethod = bm;
                architect.Stop();
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                architect.Stop();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (architect.isBuilding)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();
                }
                else
                    architect.Build(lines[Random.Range(0, lines.Length)]);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                architect.Append(lines[Random.Range(0, lines.Length)]);
            }
                
        }
    }
}
