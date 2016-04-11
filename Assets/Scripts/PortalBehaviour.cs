using UnityEngine;
using System.Collections;


/// <summary>
/// Portal behaviour.
/// </summary>
public class PortalBehaviour : MonoBehaviour {

	/// <summary>
	/// Enabled state of the portal.
	/// True = toggle active
	/// False = toggle inactive
	/// </summary>
	public bool portalEnabled = true;

	/// <summary>
	/// The name of the phase to be loaded when player crosses portal.
	/// Don't forget to set it in the prefab's inspector
	/// </summary>
	public string phaseToLoad = "";

	void Start(){
		
	}

	void Awake(){
		setActive(portalEnabled);
	}

	/// <summary>
	/// Raises the trigger enter event to check if player touched portal
	/// </summary>
	/// <param name="collider">Collider.</param>
	void OnTriggerEnter(Collider collider) {
		
		// Checks if player touched portal and loads the proper level if portal is enabled

		if (collider.gameObject.tag.Contains ("Player") && enabled)
			GameController.startPhase (phaseToLoad);
		
	}


	/// <summary>
	/// Sets the portal's state. If true, player will be able to used it.
	/// Otherwise, nothing will happen.
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	public void setActive(bool state){
		this.enabled = state;

		GameObject portalParticleActive   = gameObject.transform.FindChild ("PortalParticleActive")  .gameObject;
		GameObject portalParticleInactive = gameObject.transform.FindChild ("PortalParticleInactive").gameObject;

		var activeEmission   = portalParticleActive  .GetComponent<ParticleSystem>().emission;
		var inactiveEmission = portalParticleInactive.GetComponent<ParticleSystem>().emission;

		activeEmission  .enabled = state;
		inactiveEmission.enabled = !state;

	}
}
