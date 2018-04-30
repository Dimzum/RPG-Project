using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    const float combatCoolDown = 5;
    float lastattackTime;

    public float attackDelay = .6f;

    public bool InCombat { get; private set; }
    public event System.Action OnAttack;

    CharacterStats myStats;
    CharacterStats opponentStats;

    void Start() {
        myStats = GetComponent<CharacterStats>();
    }

    void Update() {
        attackCooldown -= Time.deltaTime;

        if (Time.time - lastattackTime > combatCoolDown) {
            InCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats) {
        if (attackCooldown <= 0f) {
            opponentStats = targetStats;
            if (OnAttack != null) {
                OnAttack();
            }

            attackCooldown = 1f / attackSpeed;
            InCombat = true;
            lastattackTime = Time.time;
        }
    }

    public void AttackHit_AnimationEvent() {
        opponentStats.TakeDamage(myStats.damage.GetValue());
        if (opponentStats.CurrentHealth <= 0) {
            InCombat = false;
        }
    }
}
