using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keeps track of enemy stats, loosing health and dying. */

public class EnemyStats : CharacterStats {

    public Object skeletonPrebab = Resources.Load("Prefabs/Skeleton Enemy");
    public GameObject skeletonEnemy;

	public override void Die() {
		base.Die();

		// Add ragdoll effect / death animation

		Destroy(gameObject);
    }

    public override void Respawn() {
        base.Respawn();
        
        skeletonEnemy = Instantiate(skeletonPrebab) as GameObject;
        //skeletonEnemy.transform.position = new Vector3();
    }

}
