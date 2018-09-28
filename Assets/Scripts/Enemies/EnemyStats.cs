using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    public float curHp;
    public float maxHp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(curHp <= 0){
            curHp = 0;
        }

        if (curHp > maxHp){
            curHp = maxHp;
        }
	}

    public void ReceiveDamange(float dmg){
        
        curHp -= dmg;
        print("Damage done = " + dmg);
        print("Enemy HP = " + curHp);

        if(curHp <= 0){
            Destroy(gameObject);
        }
    }
}
