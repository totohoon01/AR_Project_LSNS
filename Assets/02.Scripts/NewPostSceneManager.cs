using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Database;
using Post;
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
        string userPos = "10010010 / temp";
        UserPost post = new UserPost(message, createTime, userPos);
        //to JSON
        string json = JsonUtility.ToJson(post);

        //데이터 베이스에 연결
        DatabaseReference mRef = FirebaseDatabase.DefaultInstance.RootReference;
        mRef.Child("users").Child(GameManager.instance.userIdentifier).Child("post0").SetRawJsonValueAsync(json);
    }

    public void OnCancelButtonClick()
    {
        SceneManager.LoadScene("02.PlayScene");
    }
}
