using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ComenzarJuego(string Game)
    {
        SceneManager.LoadScene(Game);
    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aqui se cierra la aplicacion");
    }
}
