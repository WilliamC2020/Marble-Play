using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.Core;

public class Quit : MonoBehaviour
{
   public void QuitGame()
    {
        ExperienceApp.End();
    }
}
