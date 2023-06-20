using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBRPG.Inventories
{

    [CreateAssetMenu(menuName = ("TBRPG Project/InventorySystem/Character Item"))]
    public class CharacterItem : InventoryItem
    {

        // CONFIG DATA
        [Tooltip("Does this Chracter has being unlocked.")]
        [SerializeField] bool isunlocked = false;

        [SerializeField] int Level;
        [SerializeField] float AccumilatedExperincePoints = 0;




        public int GetLevel()
        {
            return Level;
        }

        public void SetLevel(int level)
        {
            Level = level;
        }

        public void SetExpreince(float experience)
        {
            AccumilatedExperincePoints = experience;
        }

        public float GetExperiencePoints()
        {
            return AccumilatedExperincePoints;
        }

        public virtual void Use(GameObject user)
        {
            Debug.Log("Using Character: " + this);
        }

        public bool isUnlocked()
        {
            return isunlocked;
        }

        public void SetUnlock(bool unlock)
        {
            isunlocked = unlock;
        }


    }

}
