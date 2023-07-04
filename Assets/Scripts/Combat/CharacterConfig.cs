using System.Collections;
using System.Collections.Generic;
using TBRPG.Inventories;
using TBRPG.Stats;
using UnityEngine;


namespace TBRPG.Combat
{
    [CreateAssetMenu(fileName = "Character", menuName = "TBRPG Project/Character", order = 0)]
    public class CharacterConfig : CharacterItem, IModifierProvider
    {
        [SerializeField] float PercentageBonus = 10f;
        [SerializeField] Character EquippedCharacterPrefab;
        [SerializeField] GameObject CharacterVariant;
        [SerializeField] AnimatorOverrideController animatorOveride;

        const string CharacterName = "Character";

        public Character SpawnChracter()
        {//SpawnChracter(Transform Spot,Animator animator)
            //DestroyOldCharacter(Spot,null);
            Character character = null;
            
            if (EquippedCharacterPrefab != null )
            {
                EquippedCharacterPrefab.tag = "Team";
                character = Instantiate(EquippedCharacterPrefab);
                character.gameObject.name = CharacterName;

            }

            //var overideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            //if (animatorOveride != null)
            //{
            //    animator.runtimeAnimatorController = animatorOveride;
            //}
            //else if (overideController != null)
            //{
            //    animator.runtimeAnimatorController = overideController.runtimeAnimatorController;
            //}

            return character;

        }

        private void DestroyOldCharacter(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(CharacterName);

            if (oldWeapon == null) { return; }

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }



        public float GetPercentageBonus()
        {
            return PercentageBonus;
        }

        public GameObject GetCharacterPrefab()
        {
            return EquippedCharacterPrefab.gameObject;
        }

        public GameObject GetCharacterVariant()
        {
            return CharacterVariant;
        }

        public AnimatorOverrideController GetOverideAnimatior()
        {
            return animatorOveride;
        }

        public IEnumerable<float> GetAdditiveModifiers(Stats.Stat stat)
        {
            if (stat == Stats.Stat.Damage)
            {
                yield return PercentageBonus;
                //  yield return currentWeapon.GetWeaponDamage();//Dual weilding this was in FIghter 
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stats.Stat stat)
        {
            if (stat == Stats.Stat.Damage)
            {
                yield return PercentageBonus;
            }
        }
    }
}


