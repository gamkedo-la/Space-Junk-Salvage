using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatchesMusicPool", menuName = "PatchesMusicPool")]
public class PatchesMusicPool : ScriptableObject {

	public PatchesMusicTrack[] musicStems;
	
}

[System.Serializable]
public class PatchesMusicTrack {
	public AudioClip musicStem;

	public bool loopingAsset;

	public float bpm;
	public float endTime;
	public float[] outTimes;
	public float fadeTime = 0.15f;
}