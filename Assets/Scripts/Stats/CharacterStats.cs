using UnityEngine;

/* Base class that player and enemies can derive from to include stats. */

public class CharacterStats : MonoBehaviour {

	// Health
	public int maxHealth = 100;
	public int CurrentHealth { get; private set; }

	public Stat damage;
	public Stat armor;

    public event System.Action<int, int> OnHealthChanged; // <max hp, current hp>

	// Set current health to max health
	// when starting the game.
	void Awake () {
		CurrentHealth = maxHealth;
	}

	// Damage the character
	public void TakeDamage (int damage) {
		// Subtract the armor value
		damage -= armor.GetValue();
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		// Damage the character
		CurrentHealth -= damage;
		Debug.Log(transform.name + " takes " + damage + " damage.");

        if (OnHealthChanged != null) {
            OnHealthChanged(maxHealth, CurrentHealth);
        }

		// If health reaches zero
		if (CurrentHealth <= 0) {
			Die();
		}
	}

	public virtual void Die () {
		// Die in some way
		// This method is meant to be overwritten
		Debug.Log(transform.name + " died.");
	}

    public virtual void Respawn() {

        Debug.Log(transform.name + " is respawning.");
    }

}
