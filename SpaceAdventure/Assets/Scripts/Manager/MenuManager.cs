using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public InputField _ipInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLocalGameValue(bool value)
    {
        Client.Instance._enCasa = value;
        ChangeIpText(value);
    }

    void ChangeIpText(bool value)
    {
        if (value)
        {
            _ipInput.placeholder.GetComponent<Text>().text = "127.0.0.1";
            _ipInput.interactable = false;
            string newIp = "ws://" + "127.0.0.1" + ":8080";
            Client.Instance.setIp(newIp);
        }
        else
        {
            _ipInput.interactable = true;
            _ipInput.placeholder.GetComponent<Text>().text = "IP del Servidor";
            _ipInput.text = "";
        }
            
    }

    public void UpdateID(InputField inputField)
    {
        int newId = int.Parse(inputField.text);
        print(newId);
        GameManager.Instance.setIdGame(int.Parse(inputField.text));
    }

}
