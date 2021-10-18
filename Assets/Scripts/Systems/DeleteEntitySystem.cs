using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Systems {
  [AlwaysSynchronizeSystem]
  [UpdateAfter(typeof(PickupSystem))]
  public class DeleteEntitySystem : JobComponentSystem {

    protected override JobHandle OnUpdate(JobHandle inputDeps) {
      EntityCommandBuffer commandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);

      Entities.WithAll<DeleteTag>()
      .ForEach((Entity ent) =>
          commandBuffer.DestroyEntity(ent)
      ).Run();

      commandBuffer.Playback(EntityManager);
      commandBuffer.Dispose();

      return default;
    }
  }
}
