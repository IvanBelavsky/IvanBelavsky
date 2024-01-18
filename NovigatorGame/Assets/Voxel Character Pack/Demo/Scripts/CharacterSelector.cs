using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


namespace VoxelCharacter
{
    public class CharacterSelector : MonoBehaviour
    {
        
        [SerializeField] private Transform charactersParent;

        [SerializeField] private GameObject[] characters;
        private int currentCharacter = 0;

        [SerializeField] private Text characterNameText;

        [System.Serializable]
        public class ActionButton
        {
            public bool isStatic = false;
            public Button button;
            public string trigger;
        }
        [SerializeField] private List<ActionButton> animationButtons;

        [SerializeField] private Color onButtonColor, offButtonColor;


        private void Awake()
        {
            foreach (ActionButton button in animationButtons)
            {
                button.button.onClick.AddListener(delegate{ ChangeAnimation(button.trigger, button.button); });
            }

            ChangeCharacter(0);
        }

        public void ChangeCharacter(int n)
        {
            if (animationButtons.Count > 0)
                ChangeAnimation("Static", (animationButtons.FirstOrDefault(btn => btn.isStatic) ?? animationButtons[0]).button);
            
            for (int i = 0 ; i < charactersParent.childCount; i++)
                Destroy(charactersParent.GetChild(i).gameObject);
            currentCharacter = (currentCharacter + n).WrapIndex(characters.Length);
            Instantiate(characters[currentCharacter], Vector3.zero, Quaternion.identity, charactersParent);
            characterNameText.text = characters[currentCharacter].name;
        }

        public void ChangeAnimation(string trigger, Button button)
        {
            if (charactersParent.childCount == 0) return;

            Animator animator = charactersParent.GetChild(0).GetComponent<Animator>();
            animator.SetTrigger(trigger);

            foreach (ActionButton btn in animationButtons)
                btn.button.image.color = offButtonColor;
            button.image.color = onButtonColor;
        }

    }

    internal static class Utils
    {
        public static int WrapIndex(this int index, int max)
        {
            if (index < 0) return max - Mathf.Abs(index);
            return index % max;
        }
    }
}
