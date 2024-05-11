using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGame;
    [SerializeField] private List<Button> variants;
    [SerializeField] private List<RoleButton> roleVariants;

    private List<int> _ = new List<int>();

    private RoleButton selected;


    private void OnEnable()
    {
        startGame.onClick.AddListener(StartGameClicked);

        foreach (var button in roleVariants)
        {
            button.Init();
            button.buttonClicked += ButtonClicked;
        }
    }
    private void OnDisable()
    {
        foreach (var button in roleVariants)
        {
            button.UnInit();
            button.buttonClicked -= ButtonClicked;
        }
    }

    private void ButtonClicked(RoleButton button)
    {
        selected = button;
        foreach (var role in roleVariants)
        {
            role.button.GetComponent<Image>().color = Color.gray;
        }
        button.button.GetComponent<Image>().color = Color.white;
    }

    private void StartGameClicked()
    {
        if (selected != null)
        {
            SceneManager.LoadScene(1);
            StaticData.Role = selected.currentRole;
        }
    }
}
[Serializable]
public class RoleButton
{
    public event Action<RoleButton> buttonClicked;
    public Button button;
    public Role role;
    public Role currentRole => role;

    public void Init() => button.onClick.AddListener(Clicked);
    public void UnInit() => button.onClick.RemoveListener(Clicked);

    private void Clicked()
    {
        buttonClicked?.Invoke(this);
    }
}