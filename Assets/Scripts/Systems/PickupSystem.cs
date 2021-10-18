using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class PickupSystem : JobComponentSystem {

  private BeginInitializationEntityCommandBufferSystem bufferSystem;
  private BuildPhysicsWorld physicsWorld;
  private StepPhysicsWorld stepPhysicsWorld;

  protected override void OnCreate() {
    bufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    physicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
    stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
  }

  protected override JobHandle OnUpdate(JobHandle inputDeps) {
    TriggerJob triggerJob = new TriggerJob {
      speedEntities = GetComponentDataFromEntity<SpeedData>(),
      entitiesToDelete = GetComponentDataFromEntity<DeleteTag>(),
      commandBuffer = bufferSystem.CreateCommandBuffer(),
    };

    return triggerJob.Schedule(stepPhysicsWorld.Simulation, ref physicsWorld.PhysicsWorld, inputDeps);
  }

  private struct TriggerJob : ITriggerEventsJob {

    public ComponentDataFromEntity<SpeedData> speedEntities;
    public ComponentDataFromEntity<DeleteTag> entitiesToDelete;
    public EntityCommandBuffer commandBuffer;

    public void Execute(TriggerEvent triggerEvent) {
      HandleDeletionTagging(triggerEvent.EntityB);
      HandleDeletionTagging(triggerEvent.EntityA);
    }

    private void HandleDeletionTagging(Entity ent) {
      if (speedEntities.HasComponent(ent)) {
        return;
      }
      if (!entitiesToDelete.HasComponent(ent)) {
        commandBuffer.AddComponent(ent, new DeleteTag());
      }
    }
  }
}
