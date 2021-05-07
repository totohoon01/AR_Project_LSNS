using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainSceneManeger : MonoBehaviour
{
    public TMP_InputField userNameInput;
    public TMP_InputField userPWInput;
    public TMP_Text aletText;

    //유저 정보를 저장할 변수 -> 싱글톤으로 구성해야함 별도의 오브젝트 및 스크립트에.
    private static string userName;
    private static string userPW;

    public void OnStartButtonClick()
    {
        if (userNameInput.text != null && userPWInput.text != null)
        {
            userName = userNameInput.text;
            userPW = userPWInput.text;
            SceneManager.LoadScene("02.PlayScene");
        }
        else
        {
            aletText.GetComponent<TMP_Text>().enabled = true;
            Invoke("ToggleAlret", 2.0f);
        }
    }
    void ToggleAlret()
    {
        aletText.GetComponent<TMP_Text>().enabled = false;
    }
}
