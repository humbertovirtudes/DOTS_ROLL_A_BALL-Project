using Unity.Entities;
using UnityEngine;

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
}
