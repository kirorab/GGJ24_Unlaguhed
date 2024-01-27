using UnityEngine;

public class PlayerInfo : Singleton<PlayerInfo>
{
    [field: SerializeField] public float health{ get; private set; }
    [field: SerializeField] public float maxHealth{ get; private set; } = 10;
    private Transform _player;
    private bool isInvincible = false;
    private float invincibleTime = 1f; // 无敌持续时间
    private float invincibleTimer;
    private SpriteRenderer[] spriteRenderers;
    private Color[] originColors;
    public Color invincibleColor = new Color(1f, 1f, 1f, 0.5f); // 半透明
    private float flashDuration = 0.1f; // 闪烁持续时间
    private float flashTimer;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this;
        EventSystem.Instance.AddListener(EEvent.BeforeLoadScene, BeforeLoadScene);
        _player = gameObject.GetComponent<Transform>();
        health = maxHealth;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        originColors = new Color[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originColors[i] = spriteRenderers[i].color;
        }
    }

    private void BeforeLoadScene()
    {
        _instance = null;
        EventSystem.Instance.RemoveListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }

    public void TakeDamage(int change)
    {
        if (isInvincible)
        {
            return;
        }
        isInvincible = true;
        invincibleTimer = invincibleTime;
        health -= change;
        FlashEffect();
        flashTimer = flashDuration;
        EventSystem.Instance.Invoke(EEvent.OnPlayerHealthChange);
    }

    public Transform GetPlayerPosition()
    {
        return _player;
    }
    
    private void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            flashTimer -= Time.deltaTime;

            if (flashTimer <= 0)
            {
                foreach (var renderer in spriteRenderers)
                {
                    renderer.enabled = !renderer.enabled;
                }
                flashTimer = flashDuration;
            }

            if (invincibleTimer <= 0)
            {
                isInvincible = false;
                SetNormalAppearance();
            }
        }
    }
    
    private void FlashEffect()
    {
        foreach (var renderer in spriteRenderers)
        {
            renderer.color = invincibleColor; // 例如，设置为半透明
        }
        
    }

    private void SetNormalAppearance()
    {
        // 恢复玩家的正常外观
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].enabled = true;
            spriteRenderers[i].color = originColors[i];
        }
    }

}
