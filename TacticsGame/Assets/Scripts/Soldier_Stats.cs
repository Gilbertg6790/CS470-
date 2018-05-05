using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_Stats : MonoBehaviour {
    //class stats
    public int hitPoints;
    public int movement;
    public int attack;
    public int defense;
    public int attackRange;

    //grid position
    public int posX;
    public int posY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int getHP()
    {
        return hitPoints;
    }

    public void setHP(int x)
    {
        hitPoints = x;
    }

    public int getMovement()
    {
        return movement;
    }

    public void setMovement(int x)
    {
        movement = x;
    }

    public int getAttack()
    {
        return attack;
    }

    public void setAttack(int x)
    {
        attack = x;
    }

    public int getDefense()
    {
        return defense;
    }

    public void setDefense(int x)
    {
        defense = x;
    }

    public int getAttackRange()
    {
        return attackRange;
    }

    public void setAttackRange(int x)
    {
        attackRange = x;
    }
}
