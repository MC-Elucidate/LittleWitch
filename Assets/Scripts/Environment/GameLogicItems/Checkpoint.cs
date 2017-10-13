using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    [ReadOnly]
    [SerializeField]
    private bool active = false;

    [SerializeField]
    private Material activeMaterial;
    [SerializeField]
    private Material inactiveMaterial;

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
            SetActive(collider.gameObject);
        }
    }

    public void SetInactive()
    {
        meshRenderer.material = inactiveMaterial;
        active = false;
    }

    private void SetActive(GameObject playerObject)
    {
        PlayerStatus player = gameObject.GetComponent<PlayerStatus>();
        player.CheckpointTouched(this);
        if (!active)
        {
            meshRenderer.material = activeMaterial;
            active = true;
        }
    }
}
