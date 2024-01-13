using GramEngine.Core.Input;
using GramEngine.ECS;
using GramEngine.ECS.Components;
using System.Numerics;
using GramEngine.Core;

namespace Interlinked.Components;

public class Player : Component
{
    private float xVelocity;
    private float yVelocity;
    private float speed;
    private Keys[] movementInputs = { Keys.W, Keys.A, Keys.S, Keys.D };

    public Player(float speed)
    {
        this.speed = speed;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        xVelocity = 0f;
        yVelocity = 0f;
        
        if (InputManager.GetKeyPressed(movementInputs[0])) //if W is pressed, move up
            yVelocity = -speed;
        if (InputManager.GetKeyPressed(movementInputs[2])) //if S is pressed, move down
            yVelocity = speed;
        if (InputManager.GetKeyPressed(movementInputs[1])) //if A is pressed, move left
            xVelocity = -speed;
        if (InputManager.GetKeyPressed(movementInputs[3])) //if D is pressed, move right
            xVelocity = speed;

        Transform.Position.Y += yVelocity * gameTime.DeltaTime;
        Transform.Position.X += xVelocity * gameTime.DeltaTime;
    }
}