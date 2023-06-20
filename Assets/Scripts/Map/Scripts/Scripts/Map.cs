using System.Threading;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace TBG.MAP
{
    public class Map : MonoBehaviour
    {
        [SerializeField] GameObject mapCan;
        [SerializeField] Sprite LockedSprite;
        [SerializeField] Sprite AvailableSprite;
        [SerializeField] Sprite FinishedSprite;
        [SerializeField] Canvas ErrorMessage;
        [SerializeField] TextMeshProUGUI ErrorMsg;

        //Demo
        [SerializeField] DemoSc demoSc;

        void Start()
        {
            for(int i = 0; i<mapDetails.Count; i++)
            {
                for(int j = 0; j<mapDetails[i].regions.Count; j++)
                    {
                       mapDetails[i].map.gameObject.transform.GetChild(j).GetChild(0).gameObject.SetActive(false);
                    }
            }
            ErrorMessage.enabled = false;
            mapCan.SetActive(false);
        }
        float currentTimeScale;
        void Update()
        {
            for(int i = 0; i<mapDetails.Count; i++)
            {
                if(mapDetails[i].mapID == MapID)
                {
                    for(int j = 0; j<mapDetails[i].regions.Count; j++)
                    {
                        if(mapDetails[i].regions[j].RegID == Reg )
                        {
                            for(int k = 0; k<mapDetails[i].regions[j].missions.Count; k++)
                            {
                                if(mapDetails[i].regions[j].missions[k].missionState==MissionState.Available)
                                {
                                     mapDetails[i].map.gameObject.transform.GetChild(j).GetChild(0).GetChild(k).gameObject.GetComponent<Image>().sprite = AvailableSprite;
                                }
                                else if(mapDetails[i].regions[j].missions[k].missionState==MissionState.Finished)
                                {
                                     mapDetails[i].map.gameObject.transform.GetChild(j).GetChild(0).GetChild(k).gameObject.GetComponent<Image>().sprite = FinishedSprite;
                                     if(k+1<mapDetails[i].regions[j].missions.Count && mapDetails[i].regions[j].missions[k+1].missionState==MissionState.Locked)
                                     {
                                        mapDetails[i].regions[j].missions[k+1].missionState = MissionState.Available;
                                     }
                                     else if(k+1==mapDetails[i].regions[j].missions.Count && j+1< mapDetails[i].regions.Count && mapDetails[i].regions[j+1].missions[0].missionState==MissionState.Locked)
                                     {
                                        mapDetails[i].regions[j+1].missions[0].missionState = MissionState.Available;
                                     }
                                     
                                }
                                else if(mapDetails[i].regions[j].missions[k].missionState==MissionState.Locked)
                                {
                                     mapDetails[i].map.gameObject.transform.GetChild(j).GetChild(0).GetChild(k).gameObject.GetComponent<Image>().sprite = LockedSprite;
                                }

                                if(mapDetails[i].regions[j].missions[k].objective.PointsNeed <= demoSc.Points)
                                {
                                    mapDetails[i].regions[j].missions[k].missionState = MissionState.Finished;
                                }

                            }
                            
                        }
                        
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                mapCan.SetActive(!mapCan.activeSelf);
                if(!mapCan.activeSelf)
                {
                    currentTimeScale = Time.timeScale;
                }
            }
            if(mapCan.activeSelf)
            {
                Time.timeScale = 0;
            }
            else if (!mapCan.activeSelf)
            {
                Time.timeScale = currentTimeScale;
            }
            //Debug.Log(Time.timeScale);
        }
        
        public void check(int MapID)
        {
            Debug.Log(MapID);
        }

        [System.Serializable]
        public struct mapDetail
        {
            public string mapName;
            public GameObject map;
            public int mapID;
            public List <Regions> regions;
        }

        [SerializeField] List <mapDetail> mapDetails;
        int MapID= 1; int Reg;

        public void MissionOnMapClicked(int MissID) 
        {
            for(int i = 0; i<mapDetails.Count; i++)
            {
                if(mapDetails[i].mapID == MapID)
                {
                    for(int j = 0; j<mapDetails[i].regions.Count; j++)
                    {
                        if(mapDetails[i].regions[j].RegID == Reg )
                        {
                            for(int k = 0; k<mapDetails[i].regions[j].missions.Count; k++)
                            {
                                if(mapDetails[i].regions[j].missions[k].MissionID == MissID)
                                {
                                    if(mapDetails[i].regions[j].missions[k].missionState == MissionState.Locked)
                                    {
                                        Debug.Log(mapDetails[i].regions[j].missions[k].name + " " + mapDetails[i].regions[j].missions[k].missionState);
                                        StartCoroutine(showError());
                                    }
                                    else
                                    {
                                       Debug.Log(mapDetails[i].regions[j].missions[k].name + " Can Play");
                                        SceneManager.LoadScene(mapDetails[i].regions[j].missions[k].sceneINDEX);
                                        
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }
        }

        IEnumerator showError()
        {
            yield return new WaitForSeconds(0.1f); 
            ErrorMessage.enabled = true;
            ErrorMsg.text = "Mission Locked";
            StartCoroutine(hideError());
        }

        IEnumerator hideError()
        {
            yield return new WaitForSeconds(0.9f); 
            ErrorMessage.enabled = false;
        }

        public void clickOnMap(int mapID)
        {
            MapID = mapID;
        }

        public void ClickOnRegion(int RegId)
        {
            Debug.Log(RegId);
            Reg = RegId;
            for(int i = 0; i<mapDetails.Count; i++)
            {
                if(mapDetails[i].mapID == MapID)
                {
                    for(int j = 0; j<mapDetails[i].regions.Count; j++)
                    {
                        if(mapDetails[i].regions[j].RegID == Reg )
                        {
                            Debug.Log(mapDetails[i].regions[j].name);
                            mapDetails[i].map.gameObject.transform.GetChild(j).GetChild(0).gameObject.SetActive(true);
                            mapDetails[i].map.gameObject.transform.GetChild(j).GetChild(1).gameObject.SetActive(true);
                            mapDetails[i].map.gameObject.transform.GetChild(j).GetChild(2).gameObject.SetActive(true);
                            mapDetails[i].map.gameObject.transform.GetChild(j).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText(mapDetails[i].regions[j].name);
                            mapDetails[i].map.gameObject.transform.GetChild(j).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText("Level " + mapDetails[i].regions[j].minLevel + " - " + "Level " + mapDetails[i].regions[j].maxLevel);
                            
                        }
                        else
                        {
                            mapDetails[i].map.gameObject.transform.GetChild(j).GetChild(0).gameObject.SetActive(false);    
                            mapDetails[i].map.gameObject.transform.GetChild(j).GetChild(1).gameObject.SetActive(false);
                            mapDetails[i].map.gameObject.transform.GetChild(j).GetChild(2).gameObject.SetActive(false);
                        }
                    }
                }
            }
        }

        public void MissionCompleter(Missions mission)
        {

        }
    }
}

