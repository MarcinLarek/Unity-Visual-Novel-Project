using CHARACTERS;
using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GraphicLayerTesting : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        GraphicPanel panel = GraphicPanelManager.instance.GetPanel("Background");
        GraphicLayer layer0 = panel.GetLayer(0, true);
        GraphicLayer layer1 = panel.GetLayer(1, true);

        yield return new WaitForSeconds(1);

        layer0.SetVideo("Graphics/BG Videos/Nebula");
        layer1.SetTexture("Graphics/BG Images/spaceshipinterior");

        yield return new WaitForSeconds(2);
        GraphicPanel cinematic = GraphicPanelManager.instance.GetPanel("Cinematic");
        GraphicLayer cinLayer = cinematic.GetLayer(0, true);

        Character Affir = CharacterManager.instance.CreateCharacter("Affir", true);

        yield return Affir.Say("Let's take a look at a picture on the cinematic layer.");

        cinLayer.SetTexture("Graphics/Gallery/pup");

        yield return DialogueSystem.instance.Say("Narrator", "we truly don't deserve dogs");

        cinLayer.Clear();

        yield return new WaitForSeconds(1);

        panel.Clear();


    }
}
