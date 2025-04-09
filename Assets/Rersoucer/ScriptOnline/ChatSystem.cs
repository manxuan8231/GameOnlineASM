using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatSystem : NetworkBehaviour
{

    public TextMeshProUGUI textMessage;
    public TMP_InputField InputFieldMessenge;
    public GameObject buttonSend;
    public GameObject canvasChat;
    //chạy sau khi nhân vật spawn ở trong mạng
    public override void Spawned()
    {
      textMessage = GameObject.Find("TextMessage").GetComponent<TextMeshProUGUI>();
      InputFieldMessenge = GameObject.Find("InputFieldMessage").GetComponent<TMP_InputField>();
      buttonSend = GameObject.Find("ButtonSend");
      buttonSend.GetComponent<Button>().onClick.AddListener(SendMessengeChat);
    
      canvasChat = GameObject.FindGameObjectWithTag("CanvasChat");
        canvasChat.SetActive(false);


    }

    public override void FixedUpdateNetwork()
    {
        //kiểm tra người dùng nhấn phím Tab thì mở chat
        base.FixedUpdateNetwork();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            canvasChat.SetActive(!canvasChat.activeSelf);
            if (canvasChat.activeSelf)
            {
                InputFieldMessenge.ActivateInputField();
            }
            else
            {
                InputFieldMessenge.DeactivateInputField();
            }
        }
    }
    public void SendMessengeChat()
    {
        var messenge = InputFieldMessenge.text;
        if(string.IsNullOrWhiteSpace(messenge)) return;
        var id = Runner.LocalPlayer.PlayerId;
        var text = $"Player {id}: {messenge}";
        RpcChat(text);
        InputFieldMessenge.text = "";

    }
    //Sources: gui tu đâu, Targets: đối tượng nhận
    [Rpc(RpcSources.All, RpcTargets.All)] //gửi thông báo cho tất cả người chơi
    public void RpcChat(string message)
    {
       textMessage.text += message + "\n";
    }
}
