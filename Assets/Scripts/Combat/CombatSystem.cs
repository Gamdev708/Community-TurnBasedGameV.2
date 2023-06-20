using TBRPG.Utils;
using TBRPG.Saving;
using TBRPG.Attributes;
using UnityEngine;

namespace TBRPG.Combat
{
    public class CombatSystem : MonoBehaviour
    {
        [SerializeField] CharacterConfig Defaultcharacter;
        //[SerializeField] Transform Spot;


        CharacterConfig currentCharacterConfig;
        //LazyValue<Character> currentWeapon;
        Animator animator;

        private void Awake()
        {
            currentCharacterConfig = Defaultcharacter;
            //currentWeapon = new LazyValue<Character>(GetInitalWeapon);
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
            var overideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (Defaultcharacter.GetOverideAnimatior() != null)
            {
                animator.runtimeAnimatorController = Defaultcharacter.GetOverideAnimatior();
            }
            else if (overideController != null)
            {
                animator.runtimeAnimatorController = overideController.runtimeAnimatorController;
            }
        }

        //public void EquipCharacter(CharacterConfig character)
        //{
        //    currentWeapon.value = AttachCharacter(character);
        //}


        //private Character GetInitalWeapon()
        //{
        //    return AttachCharacter(Defaultcharacter);
        //}
        //public Character AttachCharacter(CharacterConfig character)
        //{
        //    currentCharacterConfig = character;
        //    return character.SpawnChracter(Spot, animator);
        //}




        public CharacterConfig GetCharacterConfig()
        {
            return currentCharacterConfig;
        }

    }

}