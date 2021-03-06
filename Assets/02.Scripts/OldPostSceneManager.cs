using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Database;
public class OldPostSceneManager : MonoBehaviour
{
    //게임뷰에 보이는 컨텐츠
    public TMP_Text postAuthor;
    public TMP_Text createTime;
    public TMP_Text postContent;
    public TMP_Text textAlret;

    //유저확인
    private string userId;

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
                userId = postData["id"].ToString();
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
        if (GameManager.instance.userIdentifier == userId)
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
