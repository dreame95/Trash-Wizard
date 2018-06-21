using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour {

    public string playerName;
    public int level;

    // Health
    public float curHp;
    public float maxHp;
    // Mana
    public float curMana;
    public float maxMana;

    // Regeneration Possibly
    /*
     public float hpRegenTimer;
     public float hpRegenAmount;
     public float manaRegenTimer;
     public float manaRegenAmount;
    */
    // Attack Related
    public float baseAttackPower;
    public float curAttackPower;
    public float baseAttackSpeed;
    public float curAttackSpeed;
    public float baseDodge;
    public float curDodge;
    public float baseHitPercent;
    public float curHitPercent;
    // Exp
    public float curXp;
    public float maxXp;

    // GUI art
    public Texture hpBarTexture;
    public Texture manaBarTexture;
    public Texture barsBackgroundTexture;

    // Player GUI Bars Stats
    public float userHpBarLength;
    public float percentOfUserHp;
    public float userManaBarLength;
    public float percentOfUserMana;

    // Selected NPC/Enemy gameObject
    public GameObject selectedUnit;

    // Enemy Stats script Var
    public EnemyStats enemyStatsScript;

    public bool behindEnemy;
    public bool canAttack;

    // Use this for initialization
    void Start (){
    }

	// Update is called once per frame
	void Update () {
        // call selectTarget function if left mouse button is pressed
        if(Input.GetMouseButtonDown(0)){
            SelectTarget();
        }

        // Calculating Players bars
        if (curHp <= maxHp){
            percentOfUserHp = curHp / maxHp;
            userHpBarLength = percentOfUserHp * 100;
            percentOfUserMana = curMana / maxMana;
            userManaBarLength = percentOfUserMana * 100;
        }

        // Making sure health and mana don't exceed max values
        if(curHp > maxHp){
            curHp = maxHp;
        }

        if(curMana > maxMana){
            curMana = maxMana;
        }

        // Making sure health and mana don't drop below 0 
        if(curHp < 0){
            curHp = 0f;
        }

        if(curMana < 0){
            curMana = 0f;
        }

        if(selectedUnit != null){

            Vector3 toTarget = (selectedUnit.transform.position - transform.position).normalized;
            // Calculate if the player is within attacking distance and facing enemy
            float distance = Vector3.Distance(this.transform.position, selectedUnit.transform.position);
            Vector3 targetDir = selectedUnit.transform.position - transform.position;
            Vector3 forward = transform.forward;
            float angle = Vector3.Angle(targetDir, forward);

            if(angle > 60.0){
                canAttack = false;
            }
            else {
                if(distance < 60){
                    canAttack = true;
                }
                else {
                    canAttack = false;
                }
            }
        }

        // call Basic Attack if "1" button is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            if (selectedUnit != null && canAttack)
            {
                BasicAttack();
            }
            else {
                Debug.Log("Too far away or not facing enemy");
            }

            // TODO: Add the different spells
        }

		if (Input.GetKeyDown (KeyCode.Backspace)) {
			curHp -= 5;
		}

		if (Input.GetKeyDown (KeyCode.Period)) {
			curMana -= 5;
		}

	}

    public void OnGUI(){
		//(20, 30, 120, 70)
		GUI.DrawTexture(new Rect(20, 30, 120, 70), barsBackgroundTexture);
        GUI.DrawTexture(new Rect(30, 40, userHpBarLength, 20), hpBarTexture);
        GUI.DrawTexture(new Rect(30, 65, userManaBarLength, 20), manaBarTexture);
        GUI.Label(new Rect(50, 40, 200, 20), "" + curHp + " / " + maxHp);
        GUI.Label(new Rect(50, 65, 200, 20), "" + curMana + " / " + maxMana);
    }

    void SelectTarget(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000)){
            if(hit.transform.tag == "enemy"){
                selectedUnit = hit.transform.gameObject;

                // Grab EnemyStats Script from selected enemy
                enemyStatsScript = selectedUnit.transform.gameObject.transform.GetComponent<EnemyStats>();
            }
        }
    }

    void BasicAttack(){
        
        enemyStatsScript.ReceiveDamange(10f);
    }
}
