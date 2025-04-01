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

    //chạy sau khi nhân vật spawn ở trong mạng
    public override void Spawned()
    {
      textMessage = GameObject.Find("TextMessage").GetComponent<TextMeshProUGUI>();
      InputFieldMessenge = GameObject.Find("InputFieldMessage").GetComponent<TMP_InputField>();
      buttonSend = GameObject.Find("ButtonSend");
      buttonSend.GetComponent<Button>().onClick.AddListener(SendMessengeChat);
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
