using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth = 100;                                   // The current health the player has.
	public Transform  tran;

    void Start()
    {
        gameObject.tag = "localPlayerHealth";
    }

    void Update()
    {

    }
		
    public void adjustHealth(int amount)
    {
        // Reduce the current health by the damage amount.
        currentHealth += amount;

    }

	public void SetLocation(Vector3 position)
	{
		tran.position = position;
	}
		
}
