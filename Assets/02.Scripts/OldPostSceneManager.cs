using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public TMP_Text postContent;
    public TMP_Text textAlret;

    private DatabaseReference mRef;
    private DataSnapshot snapshot;
    private bool isLoaded = false;

    void Awake()
    {
        mRef = FirebaseDatabase.DefaultInstance.RootReference;
    }
    void Start()
    {
        //특정한 해쉬를 가진?? 데이터 로드
        ReadContents(GameManager.instance.hashCode);
        StartCoroutine(CheckLoaded());
    }

    void ReadContents(string hashCode)
    {
        mRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                snapshot = task.Result;
                isLoaded = true;
            }

        });
    }
    IEnumerator CheckLoaded()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (isLoaded)
            {
                DisplayPost();
                yield break;
            }
        }
    }
    void DisplayPost()
    {
        foreach (var data in snapshot.Children)
        {
            if (GameManager.instance.hashCode == data.Key)
            {
                //유니티에서는 안되는데 빌드하면 됨
                //약간 불러오는 딜레이있는듯. 최종본에 디폴트 제거, 한글처리
                IDictionary postData = (IDictionary)data.Value;
                postAuthor.text = $"Author: {postData["userName"].ToString()}";
                createTime.text = postData["createTime"].ToString();
                postContent.text = postData["message"].ToString();
                break;
            }
        }
    }

    public void OnDeleteButtonClick()
    {
        DeletePost(GameManager.instance.hashCode);
    }
    void DeletePost(string hashCode)
    {
        if (GameManager.instance.userIdentifier == postAuthor.text)
        {
            mRef.Child(GameManager.instance.hashCode).RemoveValueAsync();
            SceneManager.LoadScene("02.PlayScene");
        }
        else
        {
            textAlret.enabled = true;
            Invoke("OffAlret", 2.0f);
        }
    }
    void OffAlret()
    {
        textAlret.enabled = false;
    }

    public void OnCancelButtonClick()
    {
        SceneManager.LoadScene("02.PlayScene");
    }
}
