using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
  public void Grid2D ()
  {
      SceneManager.LoadScene(1);
  }

  public void Inicio()
  {
      SceneManager.LoadScene(0);

  }

  public void Salir()
  {
      Application.Quit();
  }

 
}
