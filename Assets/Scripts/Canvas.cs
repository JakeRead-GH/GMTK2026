using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Canvas : MonoBehaviour
{
    [SerializeField] ActionCard[] actionCards;
    [SerializeField] ActionCard testActionCard;
    [SerializeField] Dictionary<string, ActionCard> actionDict;
 
    private void Awake()
    {
        Instantiate(testActionCard, gameObject.transform);
    }
    public ActionCard[] ActionCards => actionCards;
}
