using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TBRPG.Inventories;
using UnityEngine;

namespace TBRPG.Combat
{
    public class Party : MonoBehaviour
    {
        [SerializeField] private List<CharacterConfig> _members = new List<CharacterConfig>();
        private int _maxMembers = 2;
        private int _maxPeakMembers = 3;

        private struct PartyRecord
        {
            public string characterConfig;
            public int MemberLimit;

        }
        public IEnumerable<CharacterConfig> GetCurrentMembers()
        {
            return _members;
        }

        public int AddMember(CharacterConfig Teammember)
        {
            if (_members.Count != 0)
            {
                if (_members.Any(p => p.GetItemID() == Teammember.GetItemID()))
                {
                    Debug.LogError("Player with ID " + Teammember.GetItemID() + " is already in the party.");
                    return 0;

                }
            }

            if (_members.Count > _maxMembers)
            {
                Debug.LogError("Cannot Add Member Cus of the LIMIT");
                return 0;
            }


            Debug.Log($"Adding party member {Teammember.GetDisplayName()}");

            _members.Add(Teammember);

            var index= _members.FindIndex(a=>a.GetItemID() == Teammember.GetItemID());

            return index;
        }


        public int RemoveMember(CharacterConfig Teammember,int index)
        {
            Debug.Log($"Removing party member {Teammember.GetDisplayName()}");
            _members.RemoveAt(index);
            return index;
        }

        public int GetPartyLimit()
        {
            return _maxMembers;
        }

        public CharacterConfig GetMemberById(string id)
        {
            return _members.FirstOrDefault(m => m.GetItemID() == id);
        }

        public void IncreasePartyLimit()
        {
            if (_maxMembers < 4)
            {
                _maxMembers++;
            }
            Debug.Log("CAnnot Increase Limit");
        }
    }
}