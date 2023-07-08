/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* public class TalkEventController : MonoBehaviour
{
    public UnityEvent OnTalk; //Esse evento é ativado quando o player interage com um NPC importante para a lore do jogo
    [SerializeField] public string[] NPCNames;//Esse array contém o nome dos NPCs principais
    private bool isInList = false;

    public void GetInformations ()
    {
        if(gameObject.GetComponent<Dialogue>().isDialogueActive == true && checkList()==true)
        {
           OnTalk?.Invoke();
        }
    }
    private bool checkList() //Verifica se o NPC existe na lista de NPCs que carregam informações.
    {
        name = gameObject.GetComponent<Dialogue>().NPCname;
        for(int i = 0; i < NPCNames.Length; i++)
            {
                if(name==NPCNames[i])
                {
                    isInList = true;
                }
                isInList = false;
            }
        return isInList;
    } 


 */
