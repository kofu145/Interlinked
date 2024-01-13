using System.Numerics;
using GramEngine.ECS;
using GramEngine.ECS.Components;
using Interlinked.Components;
using Transform = SFML.Graphics.Transform;

namespace Interlinked;
using GramEngine.Core;

public class Overworld : GameState
{
    private Entity playerEntity;
    public const float PlayerSize = 3f;
    public override void Initialize()
    {
        base.Initialize();
        playerEntity = new Entity();
        playerEntity.AddComponent(new Sprite("Content/nyplayer.png"));
        playerEntity.AddComponent(new Player(300f));
        playerEntity.Transform.Scale = new Vector2(PlayerSize, PlayerSize);
        playerEntity.Transform.Position =
            new Vector3(GameStateManager.Window.Width / 2, 
                GameStateManager.Window.Height / 2, 0);
        
        AddEntity(playerEntity);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        GameStateManager.Window.CameraPosition = playerEntity.Transform.Position.ToVec2() / 2;
    }
}