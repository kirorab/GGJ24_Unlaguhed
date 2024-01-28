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
        EventSystem.Instance.AddListener<bool>(EEvent.OnEndTurtleChoose, OnTurtleChoose);
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
                yield return new WaitForSeconds(1f);
                TurtleState = ETurtleState.Idle;
                EventSystem.Instance.Invoke(EEvent.OnTurtleChoose);
                killTurtleChoosed = true;
                yield break;
            }
            yield return new WaitForSeconds(weakDuration);
        }
    }

    private void OnTurtleChoose(bool isForgive)
    {
        if (isForgive)
        {
            GetForgiven();
        }
        else
        {
            currentCoroutine = StartCoroutine(RestartAttack());
        }
    }

    private IEnumerator RestartAttack()
    {
        TurtleState = ETurtleState.Weak;
        yield return new WaitForSeconds(weakDuration);
        StartAttack();
    }

    public void GetHurt()
    {
        StopCoroutine(currentCoroutine);
        if (curStage == 2)
        {
            gameObject.SetActive(false);
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

    private void GetForgiven()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Turtle"), true);
        TurtleState = ETurtleState.Follow;
        EventSystem.Instance.Invoke(EEvent.OnEndTurtleBattle);
        EventSystem.Instance.AddListener(EEvent.OnTriggerPokemonBattle, () => gameObject.SetActive(false));
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

    public void Cheat()
    {
        foreach (TurtleShell turtleShell in turtleShells)
        {
            Destroy(turtleShell.gameObject);
        }
        turtleShells.Clear();
        StopCoroutine(currentCoroutine);
        GetForgiven();
    }
}
