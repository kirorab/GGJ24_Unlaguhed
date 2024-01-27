using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETurtleState
{
    Idle,
    Attack,
    Weak,
    RetractShell,
    Rush,
    Follow,
}

public class Turtle : MonoBehaviour
{
    public GameObject turtleShellPrefab;

    public int[] turtleShellCounts;
    public float throwInterval;
    public float weakDuration;
    public float waitDelayTime;
    public int[] collisionCounts;
    public float moveSpeed;
    public float rushSpeed;
    public float followSpeed;
    public float followDist;

    private Rigidbody2D rb2d;
    private Animator animator;
    private Coroutine currentCoroutine;
    private List<TurtleShell> turtleShells;
    private bool towardsRight;
    private int curCollisionCount;
    private int curStage;
    private bool killTurtleChoosed;

    public ETurtleState TurtleState { private set; get; }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        turtleShells = new List<TurtleShell>();
        curStage = 0;
        killTurtleChoosed = false;
        TurtleState = ETurtleState.Idle;

        EventSystem.Instance.AddListener(EEvent.OnStartTurtleBattle, StartAttack);
        EventSystem.Instance.AddListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }

    private void Update()
    {
        if (TurtleState == ETurtleState.Idle || TurtleState == ETurtleState.RetractShell)
        {
            rb2d.velocity = Vector2.zero;
        }
        else if (TurtleState == ETurtleState.Attack || TurtleState == ETurtleState.Weak || TurtleState == ETurtleState.Rush)
        {
            rb2d.velocity = (towardsRight ? Vector2.right : Vector2.left) * 
                (TurtleState == ETurtleState.Rush ? rushSpeed : moveSpeed);
            transform.localScale = new Vector3(-Mathf.Sign(rb2d.velocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1f);
        }
        else if (TurtleState == ETurtleState.Follow)
        {
            if (transform.position.x < PlayerInfo.Instance.transform.position.x - followDist)
            {
                animator.Play("Walk");
                rb2d.velocity = Vector2.right * followSpeed;
            }
            else if (transform.position.x > PlayerInfo.Instance.transform.position.x + followDist)
            {
                animator.Play("Walk");
                rb2d.velocity = Vector2.left * followSpeed;
            }
            else
            {
                animator.Play("Idle");
                rb2d.velocity = Vector2.zero;
            }
            transform.localScale = new Vector3(
                Mathf.Sign(transform.position.x - PlayerInfo.Instance.transform.position.x) 
                * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1f);
        }
    }

    public void StartAttack()
    {
        animator.Play("Walk");
        currentCoroutine = StartCoroutine(DoAttack());
    }

    private IEnumerator DoAttack()
    {
        while (true)
        {
            TurtleState = ETurtleState.Attack;
            for (int i = 0; i < turtleShellCounts[curStage]; i++)
            {
                float angle = i * (360f / turtleShellCounts[curStage]);
                TurtleShell newShell = Instantiate(turtleShellPrefab, transform).GetComponent<TurtleShell>();
                newShell.Angle = angle;
                turtleShells.Add(newShell);
            }

            while (turtleShells.Count > 0)
            {
                yield return new WaitForSeconds(throwInterval);
                turtleShells[0].SwitchStateThrowingToPlayer();
                turtleShells.RemoveAt(0);
            }
            TurtleState = ETurtleState.Weak;
            if (!killTurtleChoosed && curStage == 2)
            {
                // TODO ÈÄ¹ýÎÚ¹ê
                print("ÈÄËûÒ»Ãü");
                killTurtleChoosed = true;
            }
            yield return new WaitForSeconds(weakDuration);
        }
    }

    public void GetHurt()
    {
        StopCoroutine(currentCoroutine);
        if (curStage == 2)
        {
            // TODO ÎÚ¹êËÀÍö
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Turtle"), true);
            TurtleState = ETurtleState.Follow;
            EventSystem.Instance.Invoke(EEvent.OnEndTurtleBattle);
        }
        else
        {
            TurtleState = ETurtleState.RetractShell;
            animator.Play("ShellIdle");
            currentCoroutine = StartCoroutine(DoRetractShell());
        }
    }

    private IEnumerator DoRetractShell()
    {
        yield return new WaitForSeconds(waitDelayTime);
        DoRush();
    }

    public void GetShellPushed()
    {
        StopCoroutine(currentCoroutine);
        DoRush();
    }

    private void DoRush()
    {
        TurtleState = ETurtleState.Rush;
        animator.Play("ShellWalk");
        curCollisionCount = 0;
        towardsRight = PlayerInfo.Instance.transform.position.x < transform.position.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (TurtleState == ETurtleState.Rush)
            {
                curCollisionCount++;
                if (curCollisionCount == collisionCounts[curStage])
                {
                    curStage++;
                    StartAttack();
                }
            }
            towardsRight = !towardsRight;
        }
    }

    private void BeforeLoadScene()
    {
        EventSystem.Instance.RemoveListener(EEvent.OnStartTurtleBattle, StartAttack);
        EventSystem.Instance.RemoveListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }

    public void Cheat()
    {
        foreach (TurtleShell turtleShell in turtleShells)
        {
            Destroy(turtleShell.gameObject);
        }
        turtleShells.Clear();
        curStage = 2;
        GetHurt();
    }
}
