using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Database;
using UnityEngine.SceneManagement;

public class NewPostSceneManager : MonoBehaviour
{
    //뉴 포스트 씬에서 데이터 작성 후 Post버튼을 눌렀을때.
    //텍스트박스의 내용을 받아서, json으로 변환, 데이터베이스에 업로드(시간, 위치, 내용)
    public TMP_InputField messageBox;

    public void OnPostButtonClick()
    {
        //포스트 데이터
        string createTime = System.DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 작성");
        string message = messageBox.text;
        string userPos = "10010010 / temp"; //이거는 나중에 수정해야함. 

        //데이터 베이스에 연결
        DatabaseReference mRef = FirebaseDatabase.DefaultInstance.RootReference;
        string postKey = mRef.Push().Key;

        Dictionary<string, object> update = new Dictionary<string, object>();
        string path = postKey + "/";
        update[path + "id"] = GameManager.instance.userIdentifier;
        update[path + "createTime"] = createTime;
        update[path + "message"] = message;
        update[path + "userPos"] = userPos;
        mRef.UpdateChildrenAsync(update);
        SceneManager.LoadScene("02.PlayScene");
    }

    public void OnCancelButtonClick()
    {
        SceneManager.LoadScene("02.PlayScene");
    }
}
