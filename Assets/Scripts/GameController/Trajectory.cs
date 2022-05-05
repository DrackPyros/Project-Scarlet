using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trajectory : MonoBehaviour
{
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    [SerializeField] private Transform _obstaclesParent;
    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxFrames = 100;


    void Start()
    {
        CreatePhysicsScene();
    }
    void CreatePhysicsScene(){
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        foreach (Transform obj1 in _obstaclesParent){
            foreach (Transform obj in obj1){
                var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
                ghostObj.GetComponent<Renderer>().enabled = false;
                SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            }
        }

    }
    
    public void SimulateTraectory(GameObject coinPrefab, Vector3 pos){
        var ghostObj = Instantiate(coinPrefab, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);

        _line.positionCount = _maxFrames;

        for (int i = 0; i < _maxFrames; i++){
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);
        }
        Destroy(ghostObj.gameObject);
    }
}
