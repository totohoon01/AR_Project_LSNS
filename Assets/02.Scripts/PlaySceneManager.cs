using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Database;

public class PlaySceneManager : MonoBehaviour
{
    //information
    public TMP_Text userName;
    public GameObject[] postPrefab;

    private DatabaseReference mRef;
    private DataSnapshot snapshot;
    private bool isLoaded = false;


    void Start()
    {
        //필요한 컴포넌트 연결
        mRef = FirebaseDatabase.DefaultInstance.RootReference;

        //현재 사용자 표시
        userName.text = $"Name: {GameManager.instance.userName}";
        // OnRefreshButtonClick();
    }

    #region __CALLBACKS__


    public void OnNewPostButtonClick()
    {
        SceneManager.LoadScene("04.NewPostScene");
    }

    public void OnLogOutButtonClick()
    {
        //카메라 중복생성 방지
        var objs = GameObject.FindGameObjectsWithTag("DESTROY");
        foreach (var obj in objs)
        {
            Destroy(obj);
        }
        SceneManager.LoadScene("01.MainScene");
    }

    public void OnRefreshButtonClick()
    {
        //리프레쉬하면 기존 더미 삭제.
        GameObject[] dummys = GameObject.FindGameObjectsWithTag("DUMMY");
        foreach (var dummy in dummys)
        {
            Destroy(dummy);
        }

        print("Call");
        LoadDataFromFirebase();
        StartCoroutine(CheckLoaded());
        print("Complete");
    }
    IEnumerator CheckLoaded()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (isLoaded)
            {
                DisplayPosts();
                yield break;
            }
        }
    }
    void LoadDataFromFirebase()
    {
        //파이어베이스에서 데이터를 읽어와서 snapshot에 저장함.
        mRef.GetValueAsync().ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        snapshot = task.Result;
                        isLoaded = true;
                    }
                });
    }

    void DisplayPosts()
    {
        print("Show");
        //읽어온 데이터 중에서, 일정 거리 이내에 있는 데이터는 출력함.
        foreach (var data in snapshot.Children)
        {
            IDictionary postData = (IDictionary)data.Value;
            //놀랍게도 유령 키에 접근하려 한다.
            if (postData["userPos"] != null)
            {
                string[] postPos = postData["userPos"].ToString().Split(',');
                float postN = 0;
                float postE = 0;
                float.TryParse(postPos[0], out postN);
                float.TryParse(postPos[0], out postE);
                float longitude = GameManager.instance.logitude - postN;
                float latitude = GameManager.instance.latitude - postE;
                // if (Math.Abs(longitude) <= 0.001 && Math.Abs(latitude) <= 0.001)
                {
                    Vector3 genPos = new Vector3(Random.Range(-0.5f, 1.0f), Random.Range(0.0f, 0.5f), Random.Range(0.5f, 1.0f));
                    print("Create");

                    //프리팹 생성
                    int index = Random.Range(0, 5);
                    GameObject obj = Instantiate(postPrefab[index], Camera.main.transform.position + genPos, Quaternion.LookRotation(Vector3.zero));

                    //프리팹에 정보 기록
                    obj.GetComponent<PrefabGenerator>().hashCode = data.Key;
                    obj.GetComponent<PrefabGenerator>().authorName = postData["userName"].ToString();
                    obj.GetComponent<PrefabGenerator>().createTime = postData["createTime"].ToString();

                    //프리팹 위아래 텍스트
                    obj.GetComponent<PrefabGenerator>().authorNameText.text = obj.GetComponent<PrefabGenerator>().authorName;
                    obj.GetComponent<PrefabGenerator>().creteTimeText.text = obj.GetComponent<PrefabGenerator>().createTime;
                }
            }
        }
    }
    #endregion
}