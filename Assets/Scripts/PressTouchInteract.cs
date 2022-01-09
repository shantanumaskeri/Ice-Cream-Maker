using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PressTouchInteract : MonoBehaviour
{

	public SpriteRenderer handMain;
	public SpriteRenderer handWithCup;
	public SpriteRenderer handWithSpoon;
	public SpriteRenderer handWithTweezer;
	public SpriteRenderer handWithBottle;

	public SpriteRenderer cupOnStand;
	
	public SpriteRenderer spoonInBowl;
	public SpriteRenderer scoopInSpoon;

	public SpriteRenderer bottleOnCounter;

	public SpriteRenderer flavour1SingleScoop;
	public SpriteRenderer flavour1DoubleScoop_1;
	public SpriteRenderer flavour1DoubleScoop_2;
	
	public SpriteRenderer tweezerOnCounter;

	public SpriteRenderer flavour1SingleScoopInHand;
	public SpriteRenderer flavour1DoubleScoop_1_InHand;
	public SpriteRenderer flavour1DoubleScoop_2_InHand;
	public SpriteRenderer jarTopping_1_InHand;
	public SpriteRenderer syrupInHand;
	public SpriteRenderer topping1InTweezer;

	public SpriteRenderer jarTopping_1;
	public SpriteRenderer syrup;
	public SpriteRenderer whipCream;
	public SpriteRenderer singleTopping_1;
	public SpriteRenderer doubleTopping_1_1;
	public SpriteRenderer doubleTopping_1_2;

	public GameObject spoonOnCounter;

	public BoxCollider2D spoonPointCollider;
	public BoxCollider2D whipCreamPointCollider;
	public BoxCollider2D tweezerPointCollider;
	public BoxCollider2D handCollider;
	public BoxCollider2D waterBowlCollider;
	public BoxCollider2D emptyStandCollider;

	public GameManager gameManager;

	private void Start()
	{
		spoonPointCollider.enabled = false;
		whipCreamPointCollider.enabled = false;
		tweezerPointCollider.enabled = false;

		gameManager.userInterface.UpdateInstructions(48, "Move finger on screen to move the hand. Tap to pickup, place or add things.\n\nPick up any one of the cups from the counter.");
	}

	private void Update()
	{
		InteractableCollision();
	}

	private void InteractableCollision()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Cursor.visible = false;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

			if (hit.collider != null)
			{
				//Debug.Log(hit.collider.gameObject.tag + " : " + gameManager.currentStatus);
				if (hit.collider.gameObject.tag == "Empty Cup")
				{
					if (gameManager.GetState() == "Step 1")
					{
						hit.collider.gameObject.SetActive(false);

						PickUpEmptyCup();

						gameManager.userInterface.UpdateInstructions(64, "Well Done\n\nNow place the cup on the stand.");
					}
				}
				else if (hit.collider.gameObject.tag == "Empty Stand")
				{
					hit.collider.enabled = false;

					if (gameManager.GetState() == "Step 2")
					{
						PlaceEmptyCupOnStand();

						gameManager.userInterface.UpdateInstructions(64, "Good going\n\nPick up the ice cream spoon on the right.");
					}
					else if (gameManager.GetState() == "Step 12")
					{
						if (gameManager.numberOfScoops == 1)
						{
							PlaceFilledCupOnStand();

							gameManager.userInterface.UpdateInstructions(64, "You're a natural\n\nPick up the whipped cream bottle.");
						}
					}
					else if (gameManager.GetState() == "Step 14")
					{
						if (gameManager.numberOfScoops == 2)
						{
							PlaceFilledCupOnStand();

							gameManager.userInterface.UpdateInstructions(64, "You're a natural\n\nPick up the whipped cream bottle.");
						}
					}
				}
				else if (hit.collider.gameObject.tag == "Empty Spoon")
				{
					if (gameManager.GetState() == "Step 3")
					{
						hit.collider.gameObject.SetActive(false);

						PickUpSpoonFromCounter();

						gameManager.userInterface.UpdateInstructions(64, "Great\n\nPlace spoon in the hot water bowl.");
					}
				}
				else if (hit.collider.gameObject.tag == "Water Bowl")
				{
					if (gameManager.GetState() == "Step 4")
					{
						PlaceSpoonInBowl();

						gameManager.userInterface.UpdateInstructions(64, "You're getting a hang of this\n\nRemove the spoon after 5 seconds.");
					}
				}
				else if (hit.collider.gameObject.tag == "Bowl Spoon")
				{
					if (gameManager.GetState() == "Step 5")
					{
						PickUpSpoonFromBowl();

						gameManager.userInterface.UpdateInstructions(64, "Okay...\n\nNow take a scoop of ice cream.");
					}
				}
				else if (hit.collider.gameObject.tag == "Flavour Picker 1")
				{
					if (gameManager.GetState() == "Step 6" || gameManager.GetState() == "Step 8")
					{
						ScoopFlavourInSpoon();

						gameManager.userInterface.UpdateInstructions(64, "So next...\n\nAdd the scoop in your cup.");
					}
				}
				else if (hit.collider.gameObject.tag == "Stand Cup")
				{
					if (gameManager.GetState() == "Step 7" || gameManager.GetState() == "Step 9")
					{
						if (handWithSpoon.enabled)
						{
							AddFlavourInCup();

							if (gameManager.numberOfScoops == 1)
							{
								gameManager.userInterface.UpdateInstructions(64, "Cool\n\nNow add another scoop or put the spoon down.");
							}
							else if (gameManager.numberOfScoops == 2)
							{
								handWithSpoon.gameObject.transform.localPosition = new Vector3(0, 0, 0);

								gameManager.userInterface.UpdateInstructions(64, "Keep going\n\nNow put the spoon down.");
							}
						}
					}
					if (gameManager.GetState() == "Step 9" || gameManager.GetState() == "Step 11")
					{
						if (handMain.enabled)
						{
							PickUpFilledCup();

							gameManager.userInterface.UpdateInstructions(64, "Sweeet\n\nTake the cup to the jar and add the nuts.");
						}
					}
					if (gameManager.GetState() == "Step 14" || gameManager.GetState() == "Step 16")
					{
						if (handWithBottle.enabled)
						{
							AddWhipCreamOnCup();

							gameManager.userInterface.UpdateInstructions(64, "Nice\n\nNow place the whipped cream bottle back.");
						}
					}
					if (gameManager.GetState() == "Step 18" || gameManager.GetState() == "Step 20" || gameManager.GetState() == "Step 22")
					{
						if (handWithTweezer.enabled)
						{
							AddToppingOnCup();

							if (gameManager.numberOfCherries == 1)
							{
								gameManager.userInterface.UpdateInstructions(64, "Almost there\n\nNow pick up another cherry or put the tweezer down.");
							}
							else if (gameManager.numberOfCherries == 2)
							{
								gameManager.userInterface.UpdateInstructions(64, "One last step to go...\n\nNow put the tweezer down.");
							}
						}
					}
				}
				else if (hit.collider.gameObject.tag == "Spoon Point")
				{
					if (gameManager.GetState() == "Step 8" || gameManager.GetState() == "Step 10")
					{
						DropSpoonOnCounter();

						gameManager.userInterface.UpdateInstructions(64, "Keep going\n\nNow pick up the cup from stand.");
					}
				}
				else if (hit.collider.gameObject.tag == "Cocoa Jar")
				{
					if (gameManager.GetState() == "Step 10" || gameManager.GetState() == "Step 12")
					{
						PlaceNutsOnCup();

						gameManager.userInterface.UpdateInstructions(64, "Good\n\nNow take your cup and add chocolate syrup.");
					}
				}
				else if (hit.collider.gameObject.tag == "Syrup Dispenser")
				{
					if (gameManager.GetState() == "Step 11" || gameManager.GetState() == "Step 13")
					{
						emptyStandCollider.enabled = true;

						AddSyrupOnCup();

						gameManager.userInterface.UpdateInstructions(64, "Super\n\nNow place the cup back on the stand.");
					}
				}
				else if (hit.collider.gameObject.tag == "Whip Cream Bottle")
				{
					if (gameManager.GetState() == "Step 13" || gameManager.GetState() == "Step 15")
					{
						if (handMain.enabled)
						{
							PickUpWhipCreamBottle();

							gameManager.userInterface.UpdateInstructions(64, "Well Done\n\nNow add a dollop of whip cream on your cup.");
						}
					}
				}
				else if (hit.collider.gameObject.tag == "Whip Cream Point")
				{
					if (gameManager.GetState() == "Step 15" || gameManager.GetState() == "Step 17")
					{
						PlaceWhipCreamBottle();

						gameManager.userInterface.UpdateInstructions(64, "Woohoo\n\nPick up the tweezer.");
					}
				}
				else if (hit.collider.gameObject.tag == "Empty Tweezer")
				{
					if (gameManager.GetState() == "Step 16" || gameManager.GetState() == "Step 18")
					{
						PickUpTweezer();

						gameManager.userInterface.UpdateInstructions(64, "Good going\n\nNow pick up a cherry with the tweezer.");
					}
				}
				else if (hit.collider.gameObject.tag == "Topping Picker 1")
				{
					if (gameManager.GetState() == "Step 17" || gameManager.GetState() == "Step 19" || gameManager.GetState() == "Step 21")
					{
						if (gameManager.numberOfCherries < 2)
						{
							PickUpTopping();

							gameManager.userInterface.UpdateInstructions(64, "Good going\n\nNow add the cherry topping on your cup.");
						}
					}
				}
				else if (hit.collider.gameObject.tag == "Tweezer Point")
				{
					if (gameManager.numberOfCherries > 0)
					{
						PlaceTweezerOnCounter();

						gameManager.userInterface.UpdateInstructions(64, "All Done\n\nNow serve the ice cream to your customer.");
					}
				}
			}
		}
	}

	private void PickUpEmptyCup()
	{
		handMain.enabled = false;
		handWithCup.enabled = true;

		handCollider.offset = new Vector2(0.0f, 0.8f);
		handCollider.size = new Vector2(2.0f, 1.8f);

		gameManager.SetState("Step 2");
	}

	private void PlaceEmptyCupOnStand()
	{
		handMain.enabled = true;
		handWithCup.enabled = false;
		cupOnStand.enabled = true;

		handCollider.offset = new Vector2(0.3f, 0.8f);
		handCollider.size = new Vector2(2.0f, 1.4f);

		cupOnStand.gameObject.GetComponent<BoxCollider2D>().enabled = true;

		gameManager.SetState("Step 3");
	}

	private void PickUpSpoonFromCounter()
	{
		handMain.enabled = false;
		handWithSpoon.enabled = true;

		handCollider.offset = new Vector2(0.3f, -0.2f);
		handCollider.size = new Vector2(1.7f, 1.2f);

		gameManager.SetState("Step 4");
	}

	private void PlaceSpoonInBowl()
	{
		handMain.enabled = true;
		handWithSpoon.enabled = false;
		spoonInBowl.enabled = true;

		waterBowlCollider.enabled = false;

		handCollider.offset = new Vector2(0.3f, 0.8f);
		handCollider.size = new Vector2(2.0f, 1.4f);

		gameManager.SetState("Step 5");

		StartCoroutine(AllowPickUpSpoon());
	}

	private IEnumerator AllowPickUpSpoon()
	{
		yield return new WaitForSeconds(5.0f);

		spoonInBowl.gameObject.GetComponent<BoxCollider2D>().enabled = true;
	}

	private void PickUpSpoonFromBowl()
	{
		spoonInBowl.enabled = false;
		handMain.enabled = false;
		handWithSpoon.enabled = true;

		handCollider.offset = new Vector2(0.3f, -0.2f);
		handCollider.size = new Vector2(1.7f, 1.2f);

		gameManager.SetState("Step 6");
	}

	private void ScoopFlavourInSpoon()
	{
		scoopInSpoon.enabled = true;

		if (gameManager.GetState() == "Step 6")
		{
			gameManager.SetState("Step 7");
		}
		else if (gameManager.GetState() == "Step 8")
		{
			gameManager.SetState("Step 9");
		}
	}

	private void AddFlavourInCup()
	{
		scoopInSpoon.enabled = false;
		spoonPointCollider.enabled = true;

		if (gameManager.GetState() == "Step 7")
		{
			flavour1SingleScoop.enabled = true;

			gameManager.numberOfScoops++;
			gameManager.SetState("Step 8");
		}
		else if (gameManager.GetState() == "Step 9")
		{
			flavour1SingleScoop.enabled = false;
			flavour1DoubleScoop_1.enabled = true;
			flavour1DoubleScoop_2.enabled = true;

			gameManager.numberOfScoops++;
			gameManager.SetState("Step 10");
		}
	}
	
	private void DropSpoonOnCounter()
	{
		handMain.enabled = true;
		handWithSpoon.enabled = false;
		spoonPointCollider.enabled = false;

		handCollider.offset = new Vector2(0.3f, 0.8f);
		handCollider.size = new Vector2(2.0f, 1.4f);

		spoonOnCounter.SetActive(true);

		if (gameManager.GetState() == "Step 8")
		{
			gameManager.SetState("Step 9");
		}
		else if (gameManager.GetState() == "Step 10")
		{
			gameManager.SetState("Step 11");
		}
	}

	private void PickUpFilledCup()
	{
		handMain.enabled = false;
		handWithCup.enabled = true;
		cupOnStand.enabled = false;
		flavour1SingleScoop.enabled = false;
		flavour1DoubleScoop_1.enabled = false;
		flavour1DoubleScoop_2.enabled = false;

		handCollider.offset = new Vector2(0.0f, 0.8f);
		handCollider.size = new Vector2(2.0f, 1.8f);

		cupOnStand.gameObject.GetComponent<BoxCollider2D>().enabled = false;

		if (gameManager.GetState() == "Step 9")
		{
			flavour1SingleScoopInHand.enabled = true;

			gameManager.SetState("Step 10");
		}
		else if (gameManager.GetState() == "Step 11")
		{
			flavour1DoubleScoop_1_InHand.enabled = true;
			flavour1DoubleScoop_2_InHand.enabled = true;

			gameManager.SetState("Step 12");
		}
	}

	private void PlaceNutsOnCup()
	{
		jarTopping_1_InHand.enabled = true;

		if (gameManager.GetState() == "Step 10")
		{
			gameManager.SetState("Step 11");
		}
		else if (gameManager.GetState() == "Step 12")
		{
			gameManager.SetState("Step 13");
		}
	}

	private void AddSyrupOnCup()
	{
		syrupInHand.enabled = true;

		if (gameManager.GetState() == "Step 11")
		{
			gameManager.SetState("Step 12");
		}
		else if (gameManager.GetState() == "Step 13")
		{
			gameManager.SetState("Step 14");
		}
	}

	private void PlaceFilledCupOnStand()
	{
		syrupInHand.enabled = false;
		jarTopping_1_InHand.enabled = false;
		flavour1SingleScoopInHand.enabled = false;
		flavour1DoubleScoop_1_InHand.enabled = false;
		flavour1DoubleScoop_2_InHand.enabled = false;
		handMain.enabled = true;
		handWithCup.enabled = false;
		cupOnStand.enabled = true;
		jarTopping_1.enabled = true;
		syrup.enabled = true;

		handCollider.offset = new Vector2(0.3f, 0.8f);
		handCollider.size = new Vector2(2.0f, 1.4f);

		cupOnStand.gameObject.GetComponent<BoxCollider2D>().enabled = true;

		if (gameManager.GetState() == "Step 12")
		{
			flavour1SingleScoop.enabled = true;

			gameManager.SetState("Step 13");
		}
		else if (gameManager.GetState() == "Step 14")
		{
			flavour1DoubleScoop_1.enabled = true;
			flavour1DoubleScoop_2.enabled = true;

			gameManager.SetState("Step 15");
		}
	}

	private void PickUpWhipCreamBottle()
	{
		handMain.enabled = false;
		handWithBottle.enabled = true;
		bottleOnCounter.enabled = false;

		handCollider.offset = new Vector2(0.6f, 0.0f);
		handCollider.size = new Vector2(1.2f, 1.4f);

		if (gameManager.GetState() == "Step 13")
		{
			gameManager.SetState("Step 14");
		}
		else if (gameManager.GetState() == "Step 15")
		{
			gameManager.SetState("Step 16");
		}
	}

	private void AddWhipCreamOnCup()
	{
		whipCream.enabled = true;
		whipCreamPointCollider.enabled = true;

		handWithBottle.gameObject.transform.localPosition = new Vector3(0, 0, 0);

		handCollider.offset = new Vector2(0.3f, 0.8f);
		handCollider.size = new Vector2(2.0f, 1.4f);

		if (gameManager.GetState() == "Step 14")
		{
			gameManager.SetState("Step 15");
		}
		else if (gameManager.GetState() == "Step 16")
		{
			gameManager.SetState("Step 17");
		}
	}

	private void PlaceWhipCreamBottle()
	{
		handWithBottle.enabled = false;
		handMain.enabled = true;
		bottleOnCounter.enabled = true;
		whipCreamPointCollider.enabled = false;

		handCollider.offset = new Vector2(0.3f, 0.8f);
		handCollider.size = new Vector2(2.0f, 1.4f);

		if (gameManager.GetState() == "Step 15")
		{
			gameManager.SetState("Step 16");
		}
		else if (gameManager.GetState() == "Step 17")
		{
			gameManager.SetState("Step 18");
		}
	}

	private void PickUpTweezer()
	{
		handMain.enabled = false;
		handWithTweezer.enabled = true;
		tweezerOnCounter.enabled = false;

		handCollider.offset = new Vector2(0.3f, -0.2f);
		handCollider.size = new Vector2(1.7f, 1.2f);

		if (gameManager.GetState() == "Step 16")
		{
			gameManager.SetState("Step 17");
		}
		else if (gameManager.GetState() == "Step 18")
		{
			gameManager.SetState("Step 19");
		}
	}

	private void PickUpTopping()
	{
		topping1InTweezer.enabled = true;

		if (gameManager.GetState() == "Step 17")
		{
			gameManager.SetState("Step 18");
		}
		else if (gameManager.GetState() == "Step 19")
		{
			gameManager.SetState("Step 20");
		}
		else if (gameManager.GetState() == "Step 21")
		{
			gameManager.SetState("Step 22");
		}
	}

	private void AddToppingOnCup()
	{
		tweezerPointCollider.enabled = true;
		topping1InTweezer.enabled = false;

		if (gameManager.GetState() == "Step 18")
		{
			gameManager.numberOfCherries = 1;

			singleTopping_1.enabled = true;

			gameManager.SetState("Step 19");
		}
		else if (gameManager.GetState() == "Step 20")
		{
			if (gameManager.numberOfScoops == 1)
			{
				gameManager.numberOfCherries = 2;

				singleTopping_1.enabled = false;
				doubleTopping_1_1.enabled = true;
				doubleTopping_1_2.enabled = true;
			}
			else
			{
				singleTopping_1.enabled = true;

				gameManager.numberOfCherries = 1;
			}

			gameManager.SetState("Step 21");
		}
		else if (gameManager.GetState() == "Step 22")
		{
			gameManager.numberOfCherries = 2;

			singleTopping_1.enabled = false;
			doubleTopping_1_1.enabled = true;
			doubleTopping_1_2.enabled = true;

			gameManager.SetState("Step 23");
		}
	}

	private void PlaceTweezerOnCounter()
	{
		handWithTweezer.enabled = false;
		handMain.enabled = true;
		tweezerOnCounter.enabled = true;
		tweezerPointCollider.enabled = false;

		handCollider.offset = new Vector2(0.3f, 0.8f);
		handCollider.size = new Vector2(2.0f, 1.4f);

		gameManager.SetState("Completed");
	}

}