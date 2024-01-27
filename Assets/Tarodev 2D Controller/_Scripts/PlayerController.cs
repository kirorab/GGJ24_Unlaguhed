using System;
using UnityEngine;

namespace TarodevController
{
    /// <summary>
    /// Hey!
    /// Tarodev here. I built this controller as there was a severe lack of quality & free 2D controllers out there.
    /// I have a premium version on Patreon, which has every feature you'd expect from a polished controller. Link: https://www.patreon.com/tarodev
    /// You can play and compete for best times here: https://tarodev.itch.io/extended-ultimate-2d-controller
    /// If you hve any questions or would like to brag about your score, come to discord: https://discord.gg/tarodev
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private ScriptableStats _stats;
        private Rigidbody2D _rb;
        private CapsuleCollider2D _col;
        private Animator _anim;
        private FrameInput _frameInput;
        private Vector2 _frameVelocity;
        private bool _cachedQueryStartInColliders;
        private bool CanMove = true;
        #region Interface

        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;

        #endregion

        private float _time;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<CapsuleCollider2D>();
            _anim = GetComponentInChildren<Animator>();

            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
            EventSystem.Instance.AddListener<Dialogues>(EEvent.OnStartDialogue, SetCanMoveFalse);
            EventSystem.Instance.AddListener(EEvent.OnEndDialogue, SetCanMoveTrue);
            EventSystem.Instance.AddListener(EEvent.BeforeLoadScene, BeforeLoadScene);
        }

        private void BeforeLoadScene()
        {
            EventSystem.Instance.RemoveListener<Dialogues>(EEvent.OnStartDialogue, SetCanMoveFalse);
            EventSystem.Instance.RemoveListener(EEvent.OnEndDialogue, SetCanMoveTrue);
            EventSystem.Instance.RemoveListener(EEvent.BeforeLoadScene, BeforeLoadScene);
        }

        private void SetCanMoveFalse(Dialogues dia)
        {
            CanMove = false;
        }

        private void SetCanMoveTrue()
        {
            CanMove = true;
        }

        private void Update()
        {
            _time += Time.deltaTime;
            if (!CanMove)
            {
                return;
            }
            GatherInput();
            UpdateAnim();
        }

        private void GatherInput()
        {
            _frameInput = new FrameInput
            {
                JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
                JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
            };

            if (_stats.SnapInput)
            {
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
            }

            if (_frameInput.JumpDown)
            {
                _jumpToConsume = true;
                _timeJumpWasPressed = _time;
            }
        }

        private void UpdateAnim()
        {
            if (_frameInput.Move.x == 0)
            {
                _anim.Play("standby");
            }
            else
            {
                _anim.Play("run");
                transform.localScale = new Vector3(_frameInput.Move.x, 1f);
            }
        }
        
        private void FixedUpdate()
        {
            CheckCollisions();

            HandleJump();
            HandleDirection();
            HandleGravity();
            
            ApplyMovement();
        }

        #region Collisions
        
        private float _frameLeftGrounded = float.MinValue;
        private bool _grounded;

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

            // Ground and Ceiling
            bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, ~_stats.PlayerLayer);
            bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, ~_stats.PlayerLayer);

            // Hit a Ceiling
            if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

            // Landed on the Ground
            if (!_grounded && groundHit)
            {
                _grounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _endedJumpEarly = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
            }
            // Left the Ground
            else if (_grounded && !groundHit)
            {
                _grounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }

            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        #endregion

        #region JumpedOnEnemy
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy")) // 或者使用Layer来检测
            {
                if (collision.gameObject.TryGetComponent<Turtle>(out var turtle))
                {
                    if (turtle.TurtleState == ETurtleState.Weak)
                    {
                        foreach (ContactPoint2D point in collision.contacts)
                        {
                            if (point.normal.y >= 0.5f) // 确保是从上方碰到敌人
                            {
                                // 玩家踩到了乌龟
                                PerformBounce();
                                turtle.GetHurt();
                                return;
                            }
                        }
                    }
                    else if (turtle.TurtleState == ETurtleState.RetractShell)
                    {
                        turtle.GetShellPushed();
                        return;
                    }
                }
                else if (collision.gameObject.TryGetComponent<TurtleShell>(out var turtleShell))
                {
                    Destroy(turtleShell.gameObject);
                }

                //玩家以非跳踩的形式接触到敌人
                PlayerInfo.Instance.TakeDamage(1);
            }
        }

        private void PerformBounce()
        {
            ExecuteJump(_stats.bounceForce);
        }

        #endregion
        
        #region Jumping

        private bool _jumpToConsume;
        private bool _bufferedJumpUsable;
        private bool _endedJumpEarly;
        private bool _coyoteUsable;
        private float _timeJumpWasPressed;

        private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
        private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

        private void HandleJump()
        {
            if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

            if (!_jumpToConsume && !HasBufferedJump) return;

            if (_grounded || CanUseCoyote) ExecuteJump(_stats.JumpPower);

            _jumpToConsume = false;
        }

        private void ExecuteJump(float power)
        {
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = power;
            Jumped?.Invoke();
        }

        #endregion

        #region Horizontal

        private void HandleDirection()
        {
            if (_frameInput.Move.x == 0)
            {
                var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration + _stats.AirResistance;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                var targetSpeed = _frameInput.Move.x * _stats.MaxSpeed;
                var acceleration = _grounded ? _stats.Acceleration : (_stats.Acceleration - _stats.AirResistance);
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, targetSpeed, acceleration * Time.fixedDeltaTime);
            }            
        }

        #endregion

        #region Gravity

        private void HandleGravity()
        {
            if (_grounded && _frameVelocity.y <= 0f)
            {
                _frameVelocity.y = _stats.GroundingForce;
            }
            else
            {
                var inAirGravity = _stats.FallAcceleration;
                if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }
        }

        #endregion

        private void ApplyMovement() => _rb.velocity = _frameVelocity;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
        }
#endif
    }

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }

    public interface IPlayerController
    {
        public event Action<bool, float> GroundedChanged;

        public event Action Jumped;
        public Vector2 FrameInput { get; }
    }
}