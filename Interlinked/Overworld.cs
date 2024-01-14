using System.Drawing;
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
    private Entity owbg;
    public override void Initialize()
    {
        base.Initialize();
        GameStateManager.Window.BackgroundColor = Color.FromArgb(139, 180, 141);
        GameStateManager.Window.Zoom = 200f;
        
        playerEntity = new Entity();
        playerEntity.AddComponent(new Sprite("Content/nyplayer.png"));
        playerEntity.AddComponent(new Player(300f));
        playerEntity.Transform.Position =
            new Vector3(GameStateManager.Window.Width / 2, 
                GameStateManager.Window.Height / 2, 0);
        playerEntity.AddComponent(new Animation());
        playerEntity.GetComponent<Animation>().LoadTextureAtlas("Content/nyplayeridle-Sheet.png", "idle", .4f, (16, 16));
        playerEntity.GetComponent<Animation>().LoadTextureAtlas("Content/nyplayerrun-Sheet.png", "running", .13f, (16, 16));
        playerEntity.GetComponent<Animation>().SetState("idle");
        
        owbg = new Entity();
        owbg.AddComponent(new Sprite("Content/testow.png"));
        owbg.Transform.Scale = new Vector2(8, 8);
        owbg.Transform.Position = new Vector3(GameStateManager.Window.Width / 2, 
                GameStateManager.Window.Height / 2, -1);

        AddEntity(playerEntity);
        AddEntity(owbg);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        var CameraPos = new Vector2(
            playerEntity.Transform.Position.X - GameStateManager.Window.settings.Width / 2,
            playerEntity.Transform.Position.Y - GameStateManager.Window.settings.Height / 2);
        GameStateManager.Window.CameraPosition = CameraPos;

        
    }

}