using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlaySceneManager : MonoBehaviour
{
    //AR카메라 들어갈 씬
    //씬이 로딩될때 AR카메라 켜지고,
    //카메라 영상에서 어느정도 거리에 앵커(기존 저장된 포스트)를 버튼형태로 인스턴시
    //올드 포스트를 클릭하면 -> 올드포스트 씬 로딩(데이터 가져옴)
    //뉴 포스트 버튼 클릭하면 -> 뉴포스트씬 로딩, 데이터 업로드

    public TMP_Text userName;
    void Start()
    {
        userName.text = $"Name: {GameManager.instance.userName}";
    }

    public void OnOldPostClick()
    {
        SceneManager.LoadScene("03.OldPostScene");
    }
    public void OnNewPostButtonClick()
    {
        SceneManager.LoadScene("04.NewPostScene");
    }
    public void OnLogOutButtonClick()
    {
        var objs = GameObject.FindGameObjectsWithTag("DESTROY");
        foreach (var obj in objs)
        {
            Destroy(obj);
        }
        SceneManager.LoadScene("01.MainScene");
    }
}
