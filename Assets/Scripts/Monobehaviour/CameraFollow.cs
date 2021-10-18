using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
  public Entity ballEntity;
  public float3 offset;

  private EntityManager manager;

  private void Awake() {
    manager = World.DefaultGameObjectInjectionWorld.EntityManager;
  }

  private void LateUpdate() {
    if (ballEntity == Entity.Null) { return; }

    Translation ballPos = manager.GetComponentData<Translation>(ballEntity);
    transform.position = ballPos.Value + offset;
  }
}
