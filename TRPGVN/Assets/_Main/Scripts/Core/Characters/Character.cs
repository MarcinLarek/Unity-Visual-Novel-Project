using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CHARACTERS
{
    public abstract class Character
    {
        public string name = "";
        public string displayname = "";
        public RectTransform root = null;
        public CharacterConfigData config;
        public Animator animator;

        protected CharacterManager manager => CharacterManager.instance;
        public DialogueSystem dialogueSytem => DialogueSystem.instance;

        //Coroutines
        protected Coroutine co_revealing, co_hiding;
        protected Coroutine co_moving;

        public bool isRevealing => co_revealing != null;
        public bool isHiding => co_hiding != null;
        public bool isMoving => co_moving != null;
        public virtual bool isVisible => false;

        public Character(string name, CharacterConfigData config, GameObject prefab)
        {
            this.name = name;
            displayname = name;
            this.config = config;

            Debug.Log("seekingForPrefab");
            if(prefab != null)
            {
                Debug.Log("SpawningPrefab");
                GameObject ob = Object.Instantiate(prefab, manager.characterPanel);
                ob.name = manager.FormatCharacterPath(manager.characterPrefabNameFormat, name);
                ob.SetActive(true);
                root = ob.GetComponent<RectTransform>();
                animator = root.GetComponentInChildren<Animator>();
            }
        }

        public Coroutine Say(string dialogue) => Say(new List<string> { dialogue });

        public Coroutine Say(List<string> dialogue)
        {
            dialogueSytem.ShowSpeakerName(displayname);
            UpdateTextCustomizationsOnScreen();
            return dialogueSytem.Say(dialogue);
        }

        public void SetNameColor(Color color) => config.nameColor = color;
        public void SetDialogueColor(Color color) => config.dialogueColor = color;
        public void SetNameFont(TMP_FontAsset font) => config.nameFont = font;
        public void SetDialogueFont(TMP_FontAsset font) => config.dialogueFont = font;
        public void UpdateTextCustomizationsOnScreen() => dialogueSytem.ApplySpeakerDataToDialogueContainer(config);
        public void ResetConfigurationData() => config = CharacterManager.instance.GetCharacterConfig(name);

        public virtual Coroutine Show()
        {
            if (isRevealing)
                return co_revealing;

            if (isHiding)
                manager.StopCoroutine(co_hiding);

            co_revealing = manager.StartCoroutine(ShowingOrHiding(true));

            return co_revealing;
        }
        public virtual Coroutine Hide()
        {
            if (isHiding)
                return co_hiding;

            if (isRevealing)
                manager.StopCoroutine(co_revealing);

            co_hiding = manager.StartCoroutine(ShowingOrHiding(false));

            return co_hiding;
        }
        public virtual IEnumerator ShowingOrHiding(bool show)
        {
            Debug.Log("Show/Hide cannot be called from a base character type.");
            yield return null;
        }
        public virtual void SetPosition(Vector2 position)
        {
            if (root == null)
                return;

            (Vector2 minAnchorTarget, Vector2 maxAnchorTarget) = ConvertUITargetPositionToRelativeCharacterAnchorTargets(position);
            root.anchorMin = minAnchorTarget;
            root.anchorMax = maxAnchorTarget;
        }

        public virtual Coroutine MoveToPosition(Vector2 position, float speed = 2f, bool smooth = false)
        {
            if (root == null)
                return null;

            if (isMoving)
                manager.StopCoroutine(co_moving);

            co_moving = manager.StartCoroutine(MovingToPosition(position, speed, smooth));

            return co_moving;
        }

        private IEnumerator MovingToPosition(Vector2 position, float speed, bool smooth)
        {
            (Vector2 minAnchorTarget, Vector2 maxAnchorTarget) = ConvertUITargetPositionToRelativeCharacterAnchorTargets(position);
            Vector2 padding = root.anchorMax - root.anchorMin;

            while(root.anchorMin != minAnchorTarget || root.anchorMax != maxAnchorTarget)
            {
                root.anchorMin = smooth ?
                    Vector2.Lerp(root.anchorMin, minAnchorTarget, speed * Time.deltaTime)
                    : Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed * Time.deltaTime * 0.35f);

                root.anchorMax = root.anchorMin + padding;

                if(smooth && Vector2.Distance(root.anchorMin, minAnchorTarget) <= 0.001f)
                {
                    root.anchorMin = minAnchorTarget;
                    root.anchorMax = maxAnchorTarget;
                    break;
                }

                yield return null;
            }
            Debug.Log("Done moving");
            co_moving = null;
        }

        protected (Vector2, Vector2) ConvertUITargetPositionToRelativeCharacterAnchorTargets(Vector2 position)
        {
            Vector2 padding = root.anchorMax - root.anchorMin;
            float maxX = 1f - padding.x;
            float maxY = 1f - padding.y;

            Vector2 minAnchorTarget = new Vector2(maxX * position.x, maxY * position.y);
            Vector2 maxAnchorTarget =minAnchorTarget + padding;

            return (minAnchorTarget,maxAnchorTarget);
        }

        public enum CharacterType
        {
            Text,
            Sprite,
            SpriteSheet,
            Live2D,
            Model3D
        }
    }
}