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

    private DatabaseReference mRef;

    private string hashCode = "-M_Oj1mN52AA5ZOrqbUq";

    void Awake()
    {
        mRef = FirebaseDatabase.DefaultInstance.RootReference;
        //특정한 해쉬를 가진?? 데이터 로드
    }
    void Start()
    {
        ReadContents(hashCode);
    }
    void ReadContents(string hashCode)
    {
        mRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var data in snapshot.Children)
                {
                    if (hashCode == data.Key)
                    {
                        //유니티에서는 안되는데 빌드하면 됨
                        //약간 불러오는 딜레이있는듯. 최종본에 디폴트 제거, 한글처리
                        IDictionary postData = (IDictionary)data.Value;
                        postAuthor.text = postData["id"].ToString();
                        createTime.text = postData["createTime"].ToString();
                        postContent.text = postData["message"].ToString();
                        break;
                    }
                }
            }
        });
    }
    public void OnDeleteButtonClick()
    {
        DeletePost(hashCode);
    }
    void DeletePost(string hashCode)
    {
        if (GameManager.instance.userIdentifier == postAuthor.text)
        {
            mRef.Child(hashCode).RemoveValueAsync();
        }
        SceneManager.LoadScene("02.PlayScene");
    }

    public void OnCancelButtonClick()
    {
        SceneManager.LoadScene("02.PlayScene");
    }

    // public void OnModifyButtonClick()
    // {
    //     //수정버튼(삭제할수도)
    // }

    // public void OnHideButtonClick()
    // {
    //     //현재 사용자 != 작성자 -> 해쉬값을 받아서 해당 포스트 혹은 해당 사용자 블록
    //     //해쉬 값을 받아서, 특정 사용자에게 블록
    //     //if curUser in post.hideTo()
    //     //  not Generate this
    // }


}
