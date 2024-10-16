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
            // wciúniÍcie NOWA GRA
            Loader.Load(Loader.Scene.TestScene_Maciej);
        });

        continueButton.onClick.AddListener(() => {
            // wciúniÍcie KONTYNUUJ
        });

        settingsButton.onClick.AddListener(() => {
            // wciúniÍcie USTAWIENIA
        });

        quitButton.onClick.AddListener(() => {
            // wciúniÍcie WYJDè
            Application.Quit();
        });
    }
}
