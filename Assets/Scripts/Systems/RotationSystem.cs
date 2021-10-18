using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class RotationSystem : JobComponentSystem {

  protected override JobHandle OnUpdate(JobHandle inputDeps) {
    float deltaTime = Time.DeltaTime;
    Entities.ForEach((ref Rotation rotation, in RotationSpeedData speedData) => {
      float angle = math.radians(speedData.speed * deltaTime);
      rotation.Value = math.mul(
          rotation.Value, quaternion.RotateX(angle)
      );
      rotation.Value = math.mul(
        rotation.Value, quaternion.RotateY(angle)
      );
      rotation.Value = math.mul(
        rotation.Value, quaternion.RotateZ(angle)
      );
    }).Run();

    return default;
  }
}
