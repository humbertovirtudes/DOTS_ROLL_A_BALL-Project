using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
[AddComponentMenu("Custom/Leader")]
public class LeaderAuthoring : MonoBehaviour, IConvertGameObjectToEntity {

  public GameObject followerObject;

  public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
    CameraFollow cameraFollow = followerObject.GetComponent<CameraFollow>();

    if (cameraFollow == null) {
      cameraFollow = followerObject.AddComponent<CameraFollow>();
    }

    cameraFollow.ballEntity = entity;
  }

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }
}
