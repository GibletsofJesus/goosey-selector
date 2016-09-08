using UnityEngine;
using System.Collections;

public class textureScroller : MonoBehaviour {

    public Renderer r;
    
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (r)
            r.material.mainTextureOffset = new Vector2(0, r.material.mainTextureOffset.y - Time.deltaTime);
    }
}
