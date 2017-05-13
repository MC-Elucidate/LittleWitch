using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {


    private bool active = false;
    public Material activeMaterial;
    public Material inactiveMaterial;
    private MeshRenderer meshRenderer;

	void Start () {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == Helpers.Tags.Player)
        {
            PlayerStatus player = collider.gameObject.GetComponent<PlayerStatus>();
            player.CheckpointTouched(this);
            if (!active)
            {
                meshRenderer.material = activeMaterial;
                active = true;
            }
        }
    }

    public void SetInactive()
    {
        meshRenderer.material = inactiveMaterial;
        active = false;
    }
}
