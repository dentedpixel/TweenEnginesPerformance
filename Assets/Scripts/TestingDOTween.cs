using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TestingDOTween : MonoBehaviour {
    public float showPerformanceGraphAfter = 30f;
	public int animateCount = 6000;
	public bool showVisuals = false;
	public GameObject animatePrefabVisualize;
	public GameObject animatePrefab;

	private GameObject[] cubes;

	System.Diagnostics.Stopwatch stopwatch;

	public void Awake(){
		DestroyImmediate( GameObject.Find("~LeanTween") );

		DOTween.Init(false, true, LogBehaviour.ErrorsOnly).SetCapacity(animateCount, 0);

		int iter = 0;
		int side = System.Convert.ToInt32( Mathf.Pow( animateCount*1.0f, 1.0f/3.0f ) );
		Vector3 p;
		cubes = new GameObject[ side*side*side ];
		for(int i = 0; i < side; i++){
			for(int j = 0; j < side; j++){
				for( int k = 0; k < side; k++){
					GameObject cube = showVisuals ? GameObject.Instantiate( animatePrefabVisualize ) : GameObject.Instantiate( animatePrefab );
					p = new Vector3(i*3,j*3,k*3);
					cube.transform.position = p;
					cube.name = "cube"+iter;

					cubes[iter] = cube;
					iter++;
				}
			}
		}
	}

	void Start () {
		stopwatch = new System.Diagnostics.Stopwatch();
		stopwatch.Start();

        for(int i = 0; i < cubes.Length; i++){
			GameObject cube = cubes[i];
			float delay = i*1.0f/animateCount + 1f;
			cube.transform.DOMove(-cube.transform.position, 1f).SetEase(Ease.InOutQuad).SetDelay(delay).SetLoops(128, LoopType.Yoyo);
		}
	}
	
	void FixedUpdate(){
		if(stopwatch.Elapsed.Seconds>=showPerformanceGraphAfter && showPerformanceGraphAfter!=0)
			showGraph();
	}

	void showGraph(){
		stopwatch.Stop();
		showPerformanceGraphAfter = 0;
		Debug.Log("Time elapsed:"+ stopwatch.Elapsed);
		DentedPixelPerformance.FPSGraphC fpsGraph = gameObject.GetComponent<DentedPixelPerformance.FPSGraphC>();
		fpsGraph.showPerformance();
	}

	IEnumerator gotoNextScene(float delayTime){
		yield return new WaitForSeconds(delayTime);
		SceneManager.LoadScene(0);
	}
}
