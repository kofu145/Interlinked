using GramEngine.Core.Input;
using GramEngine.ECS;
using GramEngine.ECS.Components;
using System.Numerics;
using GramEngine.Core;

namespace Interlinked.Components;

public class Player : Component
{
    private float speed;
    private Keys[] movementInputs = { Keys.W, Keys.A, Keys.S, Keys.D };
    private Animation anim;
    public const float PlayerSize = 5f;

    public Player(float speed)
    {
        this.speed = speed;
    }
    public override void Initialize()
    {
        base.Initialize();
        ParentEntity.Transform.Scale = new Vector2(PlayerSize, PlayerSize);
        anim = ParentEntity.GetComponent<Animation>();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        Vector3 direction = Vector3.Zero;
        
        if (InputManager.GetKeyPressed(movementInputs[0])) //if W is pressed, move up
            direction.Y = -speed;
        if (InputManager.GetKeyPressed(movementInputs[2])) //if S is pressed, move down
            direction.Y = speed;
        if (InputManager.GetKeyPressed(movementInputs[1])) //if A is pressed, move left
            direction.X = -speed;
        if (InputManager.GetKeyPressed(movementInputs[3])) //if D is pressed, move right
            direction.X = speed;

        if (direction != Vector3.Zero)
        {
            direction = Vector3.Normalize(direction);
            anim.SetState("running");
        }
        else
        {
            anim.SetState("idle");
        }

        if (direction.X < 0)
            ParentEntity.Transform.Scale.X = -PlayerSize;
        else if (direction.X > 0)
            ParentEntity.Transform.Scale.X = PlayerSize;

        Transform.Position += direction * speed * gameTime.DeltaTime;
    }
}