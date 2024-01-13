using GramEngine.ECS;
using GramEngine.ECS.Components;

namespace Interlinked;
using GramEngine.Core;

public class Overworld : GameState
{
    private Entity playerEntity;
    public override void Initialize()
    {
        base.Initialize();
        playerEntity = new Entity();
        playerEntity.AddComponent(new Sprite("Content/nyplayer.png"));
        
        
        AddEntity(playerEntity);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}