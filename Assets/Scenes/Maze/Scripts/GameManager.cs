using UnityEngine;
using System.Collections;
using UnityEngine.PostProcessing;
public class GameManager : MonoBehaviour {

	public Maze mazePrefab;

	public Player playerPrefab;

	private Maze mazeInstance;

	private Player playerInstance;

    public PostProcessingProfile bloomy;

	private void Start () {
        ResetBloomAtRuntime();
		StartCoroutine(BeginGame());

	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

	private IEnumerator BeginGame () {
		Camera.main.clearFlags = CameraClearFlags.Skybox;
		Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
		playerInstance = Instantiate(playerPrefab) as Player;
		playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect(-0.35f, 0.35f, 0.75f, 0.75f);
        ChangeBloomAtRuntime();
	}

	private void RestartGame () {
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		if (playerInstance != null) {
			Destroy(playerInstance.gameObject);
		}
		StartCoroutine(BeginGame());
	}

    void ChangeBloomAtRuntime()
    {
        //copy current bloom settings from the profile into a temporary variable
        BloomModel.Settings bloomSettings = bloomy.bloom.settings;

        //change the intensity in the temporary settings variable
        bloomSettings.bloom.intensity = 32;

        //set the bloom settings in the actual profile to the temp settings with the changed value
        bloomy.bloom.settings = bloomSettings;
    }

    void ResetBloomAtRuntime()
    {
        //copy current bloom settings from the profile into a temporary variable
        BloomModel.Settings bloomSettings = bloomy.bloom.settings;

        //change the intensity in the temporary settings variable
        bloomSettings.bloom.intensity = 2;

        //set the bloom settings in the actual profile to the temp settings with the changed value
        bloomy.bloom.settings = bloomSettings;
    }
}