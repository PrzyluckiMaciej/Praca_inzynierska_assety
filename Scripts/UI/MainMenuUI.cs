using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        newGameButton.onClick.AddListener(() => {
            // wci�ni�cie NOWA GRA
            Loader.Load(Loader.Scene.TestScene_Maciej);
        });

        continueButton.onClick.AddListener(() => {
            // wci�ni�cie KONTYNUUJ
        });

        settingsButton.onClick.AddListener(() => {
            // wci�ni�cie USTAWIENIA
        });

        quitButton.onClick.AddListener(() => {
            // wci�ni�cie WYJD�
            Application.Quit();
        });
    }
}
