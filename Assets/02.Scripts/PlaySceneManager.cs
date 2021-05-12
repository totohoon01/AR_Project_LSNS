using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Database;

using System.Threading;
using System.Threading.Tasks;

public class PlaySceneManager : MonoBehaviour
{
    //information
    public TMP_Text userName;
    public GameObject postPrefab;

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

        //위치 값 초기화
        GameObject[] reset = GameObject.FindGameObjectsWithTag("DESTROY");
        foreach (var dummy in reset)
        {
            dummy.transform.position = Vector3.zero;
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
                // Debug.Log($"{postPos[0]},{postPos[1]}");
                float postN = 0;
                float postE = 0;
                float.TryParse(postPos[0], out postN);
                float.TryParse(postPos[0], out postE);
                float longitude = GameManager.instance.logitude - postN;
                float latitude = GameManager.instance.latitude - postE;
                // if (Math.Abs(longitude) <= 0.001 && Math.Abs(latitude) <= 0.001)
                {
                    Vector3 genPos = new Vector3(Random.Range(-0.5f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.5f, 1.0f));
                    print("Create");
                    GameObject obj = Instantiate(postPrefab, genPos, Quaternion.identity);
                    obj.GetComponent<PrefabGenerator>().hashCode = data.Key;
                    obj.GetComponent<PrefabGenerator>().authorName = postData["userName"].ToString();
                    obj.GetComponent<PrefabGenerator>().createTime = postData["createTime"].ToString();
                }
            }
        }
    }
    #endregion
}