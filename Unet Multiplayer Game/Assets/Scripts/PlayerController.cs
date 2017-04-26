using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public Transform bulletPos;
    public GameObject bulletPrefab;
	
	void Update () {

        if (isLocalPlayer == false) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * v * 3 * Time.deltaTime);
        transform.Rotate(Vector3.up, 150 * h * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }

	}

    [Command]//called in client,run in server
    public void CmdFire()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 8;
        Destroy(bullet, 2);

        NetworkServer.Spawn(bullet);
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    
}
