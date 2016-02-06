using UnityEngine;
using System.Collections;

[AddComponentMenu("2D Framework/Utils/Sprite Shadows")]
[ExecuteInEditMode]
public class SpriteShadows : MonoBehaviour {
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
	public bool receiveShadows = false;

	// Use this for initialization
	void Awake () {
        Renderer renderer = GetComponent<Renderer>();
        renderer.shadowCastingMode = shadowCastingMode;
        renderer.receiveShadows = receiveShadows;
	}
}
