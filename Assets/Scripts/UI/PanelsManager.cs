using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    [Header("Paneles UI")] // paneles del menú
    public GameObject initialPanel;
    public GameObject DeckMenu, SignUpPanel, SelectPanel, createRoomContent, joinRoomContent, roomPanel;
    [Header("Controllers")]
    public SceneLoader sceneLoader;

    private void Start()
    {
        SetActivePanel(initialPanel);
    }
    #region Active Panels
    private void SetActivePanel(GameObject panel)
    {
        initialPanel.SetActive(false);
        DeckMenu.SetActive(false);
        SignUpPanel.SetActive(false);
        SelectPanel.SetActive(false);
        roomPanel.SetActive(false);

        panel.SetActive(true);
    }
    public void SetActivePanelInitial()
    {
        SetActivePanel(initialPanel);
    }
    public void SetActivePanelDeckMenu()
    {
        SetActivePanel(DeckMenu);
    }
    public void SetActivePanelSignUp()
    {
        if (GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameManager>().Deck.GetCards().Count == 0)
        {
            SetActivePanelDeckMenu();
            return;
        }
        SetActivePanel(SignUpPanel);
    }
    public void SetActivePanelSelect()
    {
        SetActivePanel(SelectPanel);
    }
    public void SetActivePanelRoom()
    {
        SetActivePanel(roomPanel);
    }
    private void SetActiveContent(GameObject ContentPanel)
    {
        createRoomContent.SetActive(false);
        joinRoomContent.SetActive(false);

        ContentPanel.SetActive(true);
    }
    public void SetActiveContentCreateRoom()
    {
        SetActiveContent(createRoomContent);
    }
    public void SetActiveContentJoinRoom()
    {
        SetActiveContent(joinRoomContent);
    }
    #endregion
    public void OnClickLoadTrainLevel()
    {
        if (GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameManager>().Deck.GetCards().Count == 0)
        {
            SetActivePanelDeckMenu();
            return;
        }
        sceneLoader.LoadScene();
    }
}
