using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugTrigger : MonoBehaviour
{
    public Vector3 playerPosition;
    public GameObject turtle;
    public GameObject pikachu;
    public Collider2D ghostPoundingTheWallTrigger;
    public Collider2D turtleBattleTrigger;
    public BlockWall blockWall;

    private void Start()
    {
        if (!GameManager.Instance.normalState)
        {
            turtle.SetActive(false);
            pikachu.SetActive(false);
            ghostPoundingTheWallTrigger.enabled = false;
            turtleBattleTrigger.enabled = false;
            blockWall.RemoveBlockWall();
            PlayerInfo.Instance.transform.position = playerPosition;
            GetComponent<Collider2D>().enabled = false;
            GameManager.Instance.normalState = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(StartBugging());
        GetComponent<Collider2D>().enabled = false;
    }

    private IEnumerator StartBugging()
    {
        GameObject player = PlayerInfo.Instance.gameObject;
        player.GetComponent<Rigidbody2D>().simulated = false;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1.5f, 0);
        CharacterSpasmController bug1 = player.AddComponent<CharacterSpasmController>();
        yield return new WaitForSeconds(3f);
        Destroy(bug1);
        int userChoose = ChinarMessage.ShowMsg("Unlaugh 未响应\n是否关闭该程序？", "Unlaugh");
        if (userChoose == 1)
        {
            GameManager.Instance.normalState = false;
            SceneSystem.Instance.LoadScene(EScene.MenuScene);
        }
        CharacterRotateController bug2 = player.AddComponent<CharacterRotateController>();
        yield return new WaitForSeconds(3f);
        Destroy(bug2);
        userChoose = ChinarMessage.ShowMsg("Unlaugh 未响应\n是否关闭该程序？", "Unlaugh");
        if (userChoose == 1)
        {
            GameManager.Instance.normalState = false;
            SceneSystem.Instance.LoadScene(EScene.MenuScene);
        }
        CharacterFlyController bug3 = player.AddComponent<CharacterFlyController>();
        yield return new WaitForSeconds(3f);
        Destroy(bug3);
        userChoose = ChinarMessage.ShowMsg("Unlaugh 未响应\n是否关闭该程序？", "Unlaugh");
        if (userChoose == 1)
        {
            GameManager.Instance.normalState = false;
            SceneSystem.Instance.LoadScene(EScene.MenuScene);
        }
        player.transform.position = playerPosition;
        player.transform.rotation = Quaternion.identity;
        player.GetComponent<Rigidbody2D>().simulated = true;
    }
}
