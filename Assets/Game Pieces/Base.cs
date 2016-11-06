using UnityEngine;
using System.Collections;
using System;

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

    public void levelUp() {
        level++;
        int r = level / 3;
        radius = r;
    }

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
