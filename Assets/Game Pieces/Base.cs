using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Assertions;

public class Base : MonoBehaviour {
    public HealthBar healthBar;
    public int level = 1;
    public int radius = 0;
    public bool active = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Asserts that the radius is set equal to r as a post condition
    public void levelUp() {
        level++;
        int r = level / 3;
        radius = r;
        Assert.AreEqual(radius, r);
    }

    //Tries to change the health of the healthBar, but catches exception should any exception be thrown by adjustHealth.
    public void changeHealth(int a) {
        try
        {
            healthBar.adjustHealth(a);
        }
        catch (Exception ex)
        {
           //do something
        }

        if (healthBar.currentHealth <= 0) {
            destroyBase();
        }

    }

    public void destroyBase() {
        active = false;
    }
}
