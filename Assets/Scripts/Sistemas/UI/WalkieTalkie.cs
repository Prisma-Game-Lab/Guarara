using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkieTalkie : MonoBehaviour
{
   public void Acusar()
   {
        //se usa o walkie talkie no quarto ele te leva para a cena de acusação
        if(SceneManager.GetActiveScene().name == "Quarto")
        {
            SceneManager.LoadScene("Acusar");
        }
        else
        {
            //o que for necessário para o diálogo aparecer
        }
   }
}
