using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour {

	static Transform customers;

	void Awake ()
	{
		customers = transform;
	}

	public static GameObject addCustomer(GameObject customer)
	{
		return GameObject.Instantiate (customer, customers);
	}
}
