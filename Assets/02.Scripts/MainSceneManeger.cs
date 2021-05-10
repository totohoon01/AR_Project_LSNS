using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainSceneManeger : MonoBehaviour
{
    public TMP_InputField userNameInput;
    public TMP_InputField userPWInput;
    public TMP_Text aletText;
    private Toggle toggle;

    void Start()
    {
        userNameInput.text = "hoon";
        userPWInput.text = "1234";

        //AR카메라 온오프 이벤트(메인씬에서만)
        toggle = GameObject.FindGameObjectWithTag("TOGGLE").GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(
            (bool isCamOn) =>
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = isCamOn;
            }
        );
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
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;
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

    public void OnLoginButtonClick()
    {
        //버튼 비활성화
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ST_BTN");
        foreach (var obj in objs)
        {
            if (obj.GetComponent<Image>() != null)
                obj.GetComponent<Image>().enabled = false;
            if (obj.GetComponent<Button>() != null)
                obj.GetComponent<Button>().enabled = false;
        }

        //UI 활성화
        objs = GameObject.FindGameObjectsWithTag("ST_BTN_INTER");
        foreach (var obj in objs)
        {
            if (obj.GetComponent<TMP_Text>() != null)
                obj.GetComponent<TMP_Text>().enabled = true;
            if (obj.GetComponent<TMP_InputField>() != null)
                obj.GetComponent<TMP_InputField>().enabled = true;
            if (obj.GetComponent<Image>() != null)
                obj.GetComponent<Image>().enabled = true;
        }
    }
}
