using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Database;

public class OldPostSceneManager : MonoBehaviour
{

    //특정 위치의 포스트 정보를 가져온다.
    //작성자, 작성시간, 작성내용 표시
    //버튼을 눌렀을 때 현재 사용자와 비교.

    // DB에 있는 특정 자료와 프리팹을 어떻게 연결할 것인가??

    public TMP_Text postAuthor;
    public TMP_Text createTime;


    void Start()
    {
        postAuthor.text = $"Author: {1111}";
    }

    public void OnModifyButtonClick()
    {
        //수정버튼(삭제할수도)
    }

    public void OnHideButtonClick()
    {
        //현재 사용자 != 작성자 -> 해쉬값을 받아서 해당 포스트 혹은 해당 사용자 블록
    }

    public void OnDeleteButtonClick()
    {
        //현재 사용자 == 작성자 -> 포스트 삭제(추가로 모달창 구현해도 좋을듯)
    }

    public void OnCancelButtonClick()
    {
        SceneManager.LoadScene("02.PlayScene");
    }
}
