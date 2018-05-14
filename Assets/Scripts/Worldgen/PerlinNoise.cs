using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise {

	long seed;
	public PerlinNoise(long seed) {
		this.seed = seed;
	}
	int random(int x, int range) {
		return (int) ((x + seed) ^ 5) % range;
	}
	public int getNoise(int x, int range, int chunkSize = 8) {
		int chunkIndex = x / chunkSize;
		float prog = (x % chunkSize) / (chunkSize * 1f);
		float leftRandom = Random.Range(chunkIndex, range);
		float rightRandom = Random.Range(chunkIndex + 1, range);

		float noise = (1 - prog) * leftRandom + prog * rightRandom;

		return (int) Mathf.Round(noise);
	}
}