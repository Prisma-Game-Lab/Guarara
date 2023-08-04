using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkieTalkie : MonoBehaviour
{
   public void Acusar()
   {
        //se usa o walkie talkie no quarto ele te leva para a cena de acusa��o
        if(SceneManager.GetActiveScene().name == "Quarto")
        {
            SceneManager.LoadScene("Acusar");
        }
        else
        {
            //o que for necess�rio para o di�logo que nega a a��o aparecer
        }

        AudioManager.instance.PlaySfx("walkieTalkie");
   }
}
