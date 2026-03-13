using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue Logs")]
public class Logs : ScriptableObject
{
    public string questID; // Unique identifier for the log
    public string questName; // Used for the NPC name
    public string description; // Used for the copy of their dialogue response
    public List<QuestObjective> objectives; // Potential additional elements

    //Called when scriptable obj is edited
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(questID))
        {
            questID = questName + Guid.NewGuid().ToString();
        }
    } 
}

[System.Serializable]
public class QuestObjective
{
    public string objectiveID; //Match with item ID that you need to collect or action you need to do
    public string description;
    public ObjectiveType type;
    public int requiredAmount;
    public int currentAmount;

    public bool IsCompleted => currentAmount >= requiredAmount;
}

public enum ObjectiveType { Custom }

[System.Serializable]
public class QuestProgress
{
    public Logs quest;
    public List<QuestObjective> objectives;

    public QuestProgress(Logs quest)
    {
        this.quest = quest;
        objectives = new List<QuestObjective>();

        //Deep copy avoid modifying original
        foreach (var obj in quest.objectives)
        {
            objectives.Add(new QuestObjective
            {
                objectiveID = obj.objectiveID,
                description = obj.description,
                type = obj.type,
                requiredAmount = obj.requiredAmount,
                currentAmount = 0
            });
        }
    }

    public bool IsCompleted => objectives.TrueForAll(o => o.IsCompleted);

    public string QuestID => quest.questID;
}
