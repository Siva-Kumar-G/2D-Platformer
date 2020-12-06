using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
   private IEnumerator Start()
   {
      yield return new WaitForSeconds(2f);
      SceneManager.LoadScene(1);
   }
}
