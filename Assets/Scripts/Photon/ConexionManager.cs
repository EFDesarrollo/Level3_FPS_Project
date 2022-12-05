using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using WebSocketSharp;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using Photon.Pun.Demo.Cockpit.Forms;
using System.ComponentModel;

public class ConexionManager : MonoBehaviourPunCallbacks
{
    #region Private Vars

    #endregion
    #region Public Vars
    [Header("Paneles UI")] // paneles del menú
    public PanelsManager panelManager;
    [Header("Botones UI")] // Boton de conexion
    public Button connectButton;
    [Header("Inputs UI")] // Input del usuario
    public TMP_InputField nicknameInput;
    public TMP_InputField roomNameInput, minPlayersInput, maxPlayersInput, joinRoomNameInput;
    [Header("Info UI")] // Objetos que contendrán información del y para el jugador
    public TextMeshProUGUI stateText;
    public TextMeshProUGUI userText;
    [Header("Room Info UI")]
    public TextMeshProUGUI roomName;
    public TextMeshProUGUI roomMinPlayers, roomMaxPlayers, roomCurrentPlayers;
    public TextMeshProUGUI roomTeam1UsersNikc, roomTeam2UsersNikc, roomMapName, roomType;

    #endregion
    #region On Click Events
    public void OnClickConectToServer()
    {

        if (!(string.IsNullOrEmpty(nicknameInput.text) || string.IsNullOrWhiteSpace(nicknameInput.text)))
            if (!PhotonNetwork.IsConnected)
            {
                ChangeState("Conecting...");
                connectButton.interactable = false;
                PhotonNetwork.ConnectUsingSettings();
            }
            else
                ChangeState("Is conected");
        else
            ChangeState("There isn't a nickname");
    }

    public void OnClickCreateRoom()
    {
        if (!(string.IsNullOrEmpty(roomNameInput.text) || string.IsNullOrWhiteSpace(roomNameInput.text)))
        {
            if (int.Parse(maxPlayersInput.text) >= int.Parse(minPlayersInput.text))
            {
                CreateRoom(roomNameInput.text, byte.Parse(maxPlayersInput.text));
                ChangeState("Created");
            }
            else
                ChangeState("Players Input Error");
        }
    }
    public void OnClickJoinRoom()
    {
        if (!(string.IsNullOrEmpty(joinRoomNameInput.text) || string.IsNullOrWhiteSpace(joinRoomNameInput.text)))
            JoinRoom(joinRoomNameInput.text);
        else
            // Informamos al usuario
            ChangeState("name error");
    }
    public void OnClickDisconect()
    {
        ChangeState("Disconecting...");
        PhotonNetwork.Disconnect();
    }
    public void OnClickLeftRoom()
    {
        ChangeState("Leaving Room");
        PhotonNetwork.LeaveRoom();
    }
    public void OnClickStartGame()
    {
        PhotonNetwork.LoadLevel(2);
    }
    #endregion
    #region Override functions

    public override void OnConnected()
    {
        //base.OnConnected();

        PhotonNetwork.NickName = nicknameInput.text;
        ChangeState("Conected!!");
        ChangeUser(PhotonNetwork.NickName);
        panelManager.SetActivePanelSelect();
        //SetActivePanel(SignUpPanel);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        ChangeState("Disconected: "+cause.ToString());
        ChangeUser("None UserName");
        panelManager.SetActivePanelSignUp();
        connectButton.interactable = true;
    }
    public override void OnCreatedRoom()
    {
        //base.OnCreatedRoom();
        ChangeState("Room Created Susefully");
        ModifyRoomPanel();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //base.OnCreateRoomFailed(returnCode, message);
        ChangeState("Create room failed: " + message);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        // Informamos al usuario
        ChangeState("Joined!");
        ModifyRoomPanel();
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        // Informamos al usuario
        ChangeState("Join Failed");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        // Informamos al usuario
        ChangeState("a player joined!");
        ModifyRoomPanel();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        // Informamos al usuario
        ChangeState("a player Left!");
        ModifyRoomPanel();
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        ChangeState("You Left the Room");
        panelManager.SetActivePanelSelect();
    }
    #endregion
    #region Methods
    public void CreateRoom(string roomName, byte maxPlayers, bool isVisible = true, bool isOpen = true)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        roomOptions.IsVisible = isVisible;
        roomOptions.IsOpen = isOpen;
        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    public void ModifyRoomPanel()
    {
        // activate panel
        //SetActivePanel(roomPanel);
        panelManager.SetActivePanelRoom();
        //actualizar informacion del panel
        //comenzamos leyendo la información.
        Player[] jugadores = PhotonNetwork.PlayerList;
        string playersText = "";
        for (int i = 0; i < jugadores.Length; i++)
        {
            playersText += jugadores[i].ActorNumber + " - " + jugadores[i].NickName + "\n";
        }
        roomTeam1UsersNikc.text = playersText;
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        roomType.text = "Room Type: " + (PhotonNetwork.CurrentRoom.IsOpen ? "Open" : "Closed");
        //TODO: De esta forma los demás usuarios no podrán ver el minimo.
        roomMinPlayers.text = minPlayersInput.text;
        roomMaxPlayers.text = PhotonNetwork.CurrentRoom.MaxPlayers.ToString();
        roomCurrentPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        //TODO: Necesito crear una forma de obtener el mapa correcto.
        roomMapName.text = "Synthwave Day";
    }
    #endregion
    #region FeedBackProcess
    /// <summary>
    /// metodo que cambiara el mensaje de estado
    /// de los paneles de introduccion al juego
    /// </summary>
    /// <param name="texto">Nuevo mensaje a colocar</param>
    private void ChangeState(string text)
    {
        stateText.text = text;
    }
    private void ChangeUser(string text)
    {
        userText.text = text;
    }

    #endregion
}
