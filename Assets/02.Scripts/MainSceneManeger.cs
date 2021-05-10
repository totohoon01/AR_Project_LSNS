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

    //for easy test
    void Start()
    {
        userNameInput.text = "hoon";
        userPWInput.text = "1234";
    }

    public void OnStartButtonClick()
    {
        ///<summary>
        ///Start 버튼을 눌렀을때, 사용자의 이름과 패스워드를 싱글턴 오브젝트에 전달한다.
        ///만약 두 인풋필드 중 하나라도 비어있을 경우 에러메시지를 띄운다.
        ///</summary>

        if (!string.IsNullOrEmpty(userNameInput.text) && !string.IsNullOrEmpty(userPWInput.text))
        {
            //사용자 구분
            GameManager.instance.userName = userNameInput.text;
            GameManager.instance.userPW = userPWInput.text;
            GameManager.instance.userIdentifier = GameManager.instance.userName + GameManager.instance.userPW;

            //다음 씬 로딩
            SceneManager.LoadScene("02.PlayScene");
        }
        else
        {
            aletText.GetComponent<TMP_Text>().enabled = true;
            Invoke("OffAlret", 2.0f);
        }
    }
    void OffAlret()
    {
        aletText.GetComponent<TMP_Text>().enabled = false;
    }
}
