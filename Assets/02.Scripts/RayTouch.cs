using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayTouch : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
    }

    void Update()
    {
        if (Camera.main != null)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10.0f, 1 << 6))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameManager.instance.hashCode = hit.transform.GetComponent<PrefabGenerator>().hashCode;
                    Destroy(hit.collider.GetComponent<BoxCollider>());
                    OnOldPostClick();
                }
            }
        }
    }
    void OnOldPostClick()
    {
        SceneManager.LoadScene("03.OldPostScene");
    }
}
