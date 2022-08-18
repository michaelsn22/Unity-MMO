using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public Player player;

    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text experienceText;
    public Text goldText;

    void Start()
    {
        player = GameObject.Find("Player(Clone)").GetComponent<Player>();
    }
    public void OpenQuestWindow()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        questWindow.SetActive(true);
        //Debug.Log("Method accessed!");
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString();
        goldText.text = quest.goldReward.ToString();
        titleText.text = quest.title;
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest.isActive = true;
        //give quest to player now:
        player.questList.Add(quest);
        //player.quest += questList;

    }
    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }
}
