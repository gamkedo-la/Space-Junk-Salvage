using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchesMusicRegion : MonoBehaviour {
	
	private bool startMusicOnTriggerEnter = false;
	private bool stopMusicOnTriggerExit = false;
	private float fadeTime = 2f;

	private bool hardOutOnTriggerEnter = false;
	private bool hardOutOnTriggerExit = false;

	public PatchesMusicPool pool;
	private PatchesMusicTrack[] tracks = new PatchesMusicTrack[0];

	public LayerMask layers;

	private void Start()
	{
		if (pool == null && tracks.Length == 0)
		{
			gameObject.SetActive(false);
		}
		else if (pool == null)
		{
			pool = new PatchesMusicPool();
			pool.musicStems = new PatchesMusicTrack[tracks.Length];
			pool.musicStems = tracks;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (0 != (layers.value & 1 << other.gameObject.layer)) {
			PatchesMusicManager.Instance.PushMusicPool(pool);
			if (startMusicOnTriggerEnter) PatchesMusicManager.Instance.StartMusic();
			if (hardOutOnTriggerEnter) PatchesMusicManager.Instance.TransitionMusicNow();
		}
	}

	void OnTriggerExit(Collider other) {
		if (0 != (layers.value & 1 << other.gameObject.layer))
			PatchesMusicManager.Instance.PopMusicPool();
		if (stopMusicOnTriggerExit) PatchesMusicManager.Instance.FadeOutMusic(fadeTime);
		if (hardOutOnTriggerExit) PatchesMusicManager.Instance.TransitionMusicNow();
	}
}
