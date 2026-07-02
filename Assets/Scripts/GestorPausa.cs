using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorPausa : MonoBehaviour
{
    public GameObject menuPausa; 
    private bool estaPausado = false;

    void Update()
    {
        // Alternar pausa con la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estaPausado)
                ReanudarJuego();
            else
                PausarJuego();
        }
    }

    public void PausarJuego()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f; // Detiene el tiempo del juego
        estaPausado = true;
    }

    public void ReanudarJuego()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        estaPausado = false;
    }

    public void IrAlMenuPrincipal(string Menu)
    {
        Time.timeScale = 1f; // Muy importante: restablecer el tiempo antes de cambiar de escena
        SceneManager.LoadScene(Menu);
    }
}

