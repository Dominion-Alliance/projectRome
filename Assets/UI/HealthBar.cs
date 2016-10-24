using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth = 100;                                   // The current health the player has.
	public Transform  tran;

    void start()
    {
        gameObject.tag = "localPlayerHealth";
    }

    void Update()
    {

    }
		
    public void AdjustHealth(int amount)
    {
        // Reduce the current health by the damage amount.
        currentHealth += amount;

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0)
        {
            // GAME OVER
        }
    }

	public void SetLocation(Vector3 position)
	{
		tran.position = position;
	}
		
}
