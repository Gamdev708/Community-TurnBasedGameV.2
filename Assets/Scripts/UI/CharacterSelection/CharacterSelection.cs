using TBRPG.Combat;
using TBRPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TBRPG.SceneManagement;
using UnityEngine.UI;
using System.Linq;

namespace TBRPG.UI
{
    public class CharacterSelection : MonoBehaviour
    {
        [Header("Characters & Specifications")]
        [SerializeField] CharacterConfig[] chracterList;
        [SerializeField,Tooltip("Spot to make the Preview Model")] Transform Spot;
        [SerializeField, Tooltip("Which Battle scene transition to")] int Scenenumber = 0;
        [SerializeField, Tooltip("Fade Out/In Time")] int Fadeout_IN_Time = 0;

        [Header("UI"), Space]
        [SerializeField] Button ChooseBtn;
        [SerializeField] Button PlayBtn;

        [Header("Player Team UI"), Space]
        [SerializeField] List<TeamUISelect> teamMembers;

        List<GameObject> Chracters;

        int CurrentCharacter = 0;

        int ActiveTeamCount = 0;

        Party party;

        private void Awake()
        {
            party = FindObjectOfType<Party>();
        }


        // Start is called before the first frame update
        void Start()
        {
            Chracters = new List<GameObject>();
            foreach (var character in chracterList)
            {
                GameObject go = Instantiate(character.GetCharacterPrefab(), Spot.position, Quaternion.identity);
                go.transform.rotation = Spot.rotation;
                go.SetActive(false);
                go.transform.SetParent(Spot);
                Chracters.Add(go);
            }

        }

        // Update is called once per frame
        void ShowChracterFromList()
        {
            Chracters[CurrentCharacter].SetActive(true);
        }

        public void OnClickNext()
        {
            Chracters[CurrentCharacter].SetActive(false);

            if (CurrentCharacter < Chracters.Count - 1)
            {
                CurrentCharacter++;
            }
            else
            {
                CurrentCharacter = 0;
            }
            ShowChracterFromList();
        }

        public void OnClickPrev()
        {
            Chracters[CurrentCharacter].SetActive(false);

            if (CurrentCharacter == 0)
            {
                CurrentCharacter = Chracters.Count - 1;
            }
            else
            {
                CurrentCharacter--;
            }
            ShowChracterFromList();
        }

        public void OnclickPlay()
        {
            GameObject.FindGameObjectWithTag("Portal").GetComponent<Portal>().CallSwitchLevelCorutine();
        }

        public void AssignTeamMember()
        {
            if (party.GetCurrentMembers().Count() <= party.GetPartyLimit())
            {
                PlayBtn.gameObject.SetActive(true);
            }

            print(chracterList[CurrentCharacter].GetItemID());
            int addedIndex=party.AddMember(chracterList[CurrentCharacter]);
            teamMembers[addedIndex].gameObject.GetComponent<TeamUISelect>().SetImage(chracterList[CurrentCharacter].GetIcon());
            teamMembers[addedIndex].gameObject.GetComponent<TeamUISelect>().SetButton(true);
            if (addedIndex==1)
            {
                teamMembers[addedIndex-1].gameObject.GetComponent<TeamUISelect>().SetButtonInteractble(false);
            }
            if (addedIndex==2)
            {
                teamMembers[addedIndex-1].gameObject.GetComponent<TeamUISelect>().SetButtonInteractble(false);
            }

        }
        public void RemoveTeamMember(int num)
        {
            if (party.GetCurrentMembers().Count() <= party.GetPartyLimit())
            {
                PlayBtn.gameObject.SetActive(false);
            }
            //if (party.GetCurrentMembers().Count() == 0)
            //{
            //    PlayBtn.gameObject.SetActive(false);
            //}

            print(chracterList[CurrentCharacter].GetItemID());
            int removedIndex =party.RemoveMember(chracterList[CurrentCharacter], num);

            teamMembers[num].SetImage(null);
            teamMembers[num].SetButton(false);

            if (removedIndex == 2)
            {
                teamMembers[removedIndex - 1].gameObject.GetComponent<TeamUISelect>().SetButtonInteractble(true);
            }
            if (removedIndex == 1)
            {
                teamMembers[removedIndex - 1].gameObject.GetComponent<TeamUISelect>().SetButtonInteractble(true);
            }
            // teamMembers[CurrentCharacter].sprite = null;
        }
        
    }
}